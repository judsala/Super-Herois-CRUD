import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Hero } from '../models/hero';

@Injectable({
  providedIn: 'root',
})
export class HeroService {
  private urlApi = 'http://localhost:5161';

  constructor(private http: HttpClient) {}

  //[GET] lista heróis
  obterHerois(): Observable<Hero[]> {
    return this.http.get<Hero[]>(`${this.urlApi}/heroes`);
  }

  //[GET] obtém heróis pelo id
  obterHeroiPorId(id: number): Observable<Hero> {
    return this.http.get<Hero>(`${this.urlApi}/heroes/${id}`);
  }

  //[POST] adiciona um novo herói
  adicionarHeroi(heroi: Hero): Observable<void> {
    return this.http.post<void>(`${this.urlApi}/heroes`, heroi);
  }

  //[PUT] atualiza um herói existente
  editarHeroi(id: number, heroi: Hero): Observable<void> {
    return this.http.put<void>(`${this.urlApi}/heroes/${id}`, heroi);
  }

  //[DELETE] exclui um herói
  excluirHeroi(id: number): Observable<void> {
    return this.http.delete<void>(`${this.urlApi}/heroes/${id}`);
  }
}
