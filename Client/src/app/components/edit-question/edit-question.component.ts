import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Select2Data, Select2SearchEvent, Select2UpdateEvent } from 'ng-select2-component';
import { Question } from 'src/app/_models/question';
import { TagLanguage } from 'src/app/_models/taglaguage';
import { QuestionService } from 'src/app/_services/question.service';
import { TagLanguageService } from 'src/app/_services/tag-language.service';

@Component({
  selector: 'app-edit-question',
  templateUrl: './edit-question.component.html',
  styleUrls: ['./edit-question.component.css']
})
export class EditQuestionComponent implements OnInit {
  markdown = '';
  languagesForm: FormGroup;
  titleQuestion = '';
  tags: TagLanguage[];
  value: any = [];
  question: Question;

  constructor(private router: Router, private questionService: QuestionService, private tagService: TagLanguageService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadTags();
    this.route.data.subscribe(data => {
      this.question = data.question;
      this.titleQuestion = this.question.tittle;
      this.markdown = this.question.noiDung;
      this.question.questionTags.forEach(model => {
        this.value.push(model.tagId);
      }) 
      this.khoiTaoForm();     
    });
  }

  loadTags() {
    this.tagService.getTags().subscribe(datav => {
      this.tags = datav;
      this.tags.forEach(model => {
        //Load data into select2-component
        this.data.push({
          value: model.id,
          label: model.name,
          data: { color: '', name: model.name }
        })
      })
    });
  }

  khoiTaoForm() {
    this.languagesForm = new FormGroup({
      title: new FormControl(this.titleQuestion, Validators.required),
      markdownText: new FormControl(this.markdown, [Validators.required, Validators.minLength(6)])
    });
  }

  public data: Select2Data = [
    // {
    //   value: '1',
    //   label: 'Angular1',
    //   data: { color: 'white', name: '' }
    // },
    // {
    //   value: '2',
    //   label: '.Net Core 5 12',
    //   data: { color: 'red', name: '' }
    // }
  ]

  updateEvent(event: Select2UpdateEvent) {
    //console.log(event.value);//event.value is array string: ['ubuntu', 'hoainam10th']
    this.value = event.value;
  }

  searchEvent(event: Select2SearchEvent) {
    //console.log(event.search);
    if (event.search.length > 0) {
      this.data = [];
      this.tagService.search(event.search).subscribe(datav => {
        this.tags = datav;
        this.tags.forEach(model => {
          //Load data into select2-component
          this.data.push({
            value: model.id,
            label: model.name,
            data: { color: '', name: model.name }
          })
        })
      });
    }
  }

  updateQuestion() {
    let model={
      id: this.question.id,
      tittle: this.titleQuestion,
      noiDung: this.markdown,
      tags: this.value
    }
    this.questionService.updateQuestion(model).subscribe(()=>{
      this.router.navigate(['/detail', this.question.id]);
    });
  }

}
