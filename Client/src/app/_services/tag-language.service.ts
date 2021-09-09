import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { TagLanguage } from '../_models/taglaguage';

@Injectable({
  providedIn: 'root'
})
export class TagLanguageService {
  baseUrl = environment.apiUrl;
  
  constructor(private http: HttpClient) { }

  getTags(){
    return this.http.get<TagLanguage[]>(this.baseUrl+'TagLaguage');
  }

  search(name: string){
    return this.http.get<TagLanguage[]>(this.baseUrl+'TagLaguage/'+name);
  }
}
