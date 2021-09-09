import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Select2Data, Select2SearchEvent, Select2UpdateEvent } from 'ng-select2-component';
import { MarkdownService } from 'ngx-markdown';
import { map } from 'rxjs/operators';
import { Question } from 'src/app/_models/question';
import { TagLanguage } from 'src/app/_models/taglaguage';
import { QuestionService } from 'src/app/_services/question.service';
import { TagLanguageService } from 'src/app/_services/tag-language.service';

@Component({
  selector: 'app-editor-question',
  templateUrl: './editor-question.component.html',
  styleUrls: ['./editor-question.component.css']
})
export class EditorQuestionComponent implements OnInit {
  markdown = `## Markdown __rulez__!
  ---
  
  ### Syntax highlight
  \`\`\`typescript
  const language = 'typescript';
  \`\`\`
  
  ### Lists
  1. Ordered list
  2. Another bullet point
     - Unordered list
     - Another unordered bullet
  
  ### Blockquote
  > Blockquote to the max`;

  languagesForm: FormGroup;
  titleQuestion = '';
  tags: TagLanguage[];

  constructor(private router: Router, private tagService: TagLanguageService, private questionService: QuestionService) { }

  ngOnInit(): void {
    this.khoiTaoForm();
    this.loadTags();
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

  //FormControl('',[Validators.required]) sẻ bị lỗi, ExpressionChangedAfterItHasBeenCheckedError
  //vì textarea đã binding data this.markdown
  khoiTaoForm() {
    this.languagesForm = new FormGroup({
      title: new FormControl(this.titleQuestion, Validators.required),
      markdownText: new FormControl(this.markdown, [Validators.required, Validators.minLength(6)])
    })
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

  dataTags: any;

  updateEvent(event: Select2UpdateEvent) {
    //console.log(event.value);//event.value is array string: ['ubuntu', 'hoainam10th']
    this.dataTags = event.value;
  }

  saveQuestion() {
    let model = {
      tittle: this.titleQuestion,
      tags: this.dataTags,
      noiDung: this.markdown
    }

    this.questionService.addQuestion(model).subscribe((data: Question) => {
      this.router.navigate(['/detail', data.id]);
    })
  }

}
