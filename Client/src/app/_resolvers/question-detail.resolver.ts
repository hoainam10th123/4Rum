import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve } from "@angular/router";
import { Observable } from "rxjs";
import { Question } from "../_models/question";
import { QuestionService } from "../_services/question.service";

@Injectable({
    providedIn: 'root'
})
export class QuestionDetailedResolver implements Resolve<Question> {

    constructor(private questionService: QuestionService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Question> {
        return this.questionService.detail(route.paramMap.get('id'));
    }
}