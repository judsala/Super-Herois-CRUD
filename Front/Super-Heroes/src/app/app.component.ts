import { Hero } from './models/hero';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { HeroService } from './services/hero.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  constructor(private heroService: HeroService) {}

  title = 'Super-Heroes';
  http = inject(HttpClient);

  //listar herois
  heroes$?: Observable<Hero[]>;

  //buscar herói
  heroiEncontrado$?: Observable<Hero>;
  valorBuscaHeroi = '';

  //adicionar heroi
  nomeAdicionar = '';
  nomeHeroiAdicionar = '';
  superpoderesAdicionar = '';
  nascimentoAdicionar = '';
  alturaAdicionar = '';
  pesoAdicionar = '';

  //listar heróis
  obterHerois() {
    this.heroes$ = this.heroService.obterHerois();
    this.heroes$.subscribe({
      error: (err) => {
        console.error('Erro ao obter lista de heróis:', err);
        alert('Não foi possível carregar a lista de heróis.');
      },
    });
  }

  //buscar heroi pelo id
  obterHeroiPorId() {
    const id = Number(this.valorBuscaHeroi); // Converte o valor para número
    if (isNaN(id) || id <= 0) {
      alert('Por favor, insira um ID válido (número positivo).');
      return;
    }

    this.heroiEncontrado$ = this.heroService.obterHeroiPorId(id);
    this.heroiEncontrado$.subscribe({
      next: (heroi) => {
        console.log('Herói encontrado:', heroi);
        alert(`Herói encontrado: ${heroi.nomeHeroi} (${heroi.nome})`);
      },
      error: (err) => {
        console.error('Erro ao buscar herói:', err);
        alert('Herói não encontrado. Verifique o ID e tente novamente.');
      },
    });
  }

  //adicionar novo heroi
  adicionarHeroi() {
    if (!this.nomeAdicionar) {
      alert('O nome do herói é obrigatório!');
      return;
    }

    const novoHeroi: Hero = {
      id: 0,
      nome: this.nomeAdicionar,
      nomeHeroi: this.nomeHeroiAdicionar,
      superpoderes: this.superpoderesAdicionar,
      dataNascimento: this.nascimentoAdicionar,
      altura: this.alturaAdicionar,
      peso: this.pesoAdicionar,
    };

    this.heroService.adicionarHeroi(novoHeroi).subscribe({
      next: () => {
        alert('Herói adicionado com sucesso!');
        this.obterHerois();
        this.limparCampos();
      },
      error: (err) => {
        console.error('Erro ao adicionar herói:', err);
        alert('Não foi possível adicionar o herói.');
      },
    });
  }

  limparCampos() {
    this.nomeAdicionar = '';
    this.nomeHeroiAdicionar = '';
    this.superpoderesAdicionar = '';
    this.nascimentoAdicionar = '';
    this.alturaAdicionar = '';
    this.pesoAdicionar = '';
  }

  //editar heroi
  editarHeroi(id: number) {
    const heroiAtualizado: Hero = {
      id,
      nome: 'Novo Nome',
      nomeHeroi: 'Novo Nome do Herói',
      superpoderes: 'Novos Superpoderes',
      dataNascimento: '2000-01-01',
      altura: '1,80m',
      peso: '80kg',
    };

    this.heroService.editarHeroi(id, heroiAtualizado).subscribe({
      next: () => {
        alert('Herói atualizado com sucesso!');
        this.obterHerois(); // Atualiza a lista de heróis
      },
      error: (err) => {
        console.error('Erro ao editar herói:', err);
        alert('Não foi possível editar o herói.');
      },
    });
  }

  //excluir heroi
  excluirHeroi(id: number) {
    if (confirm('Tem certeza que deseja excluir este herói?')) {
      this.heroService.excluirHeroi(id).subscribe({
        next: () => {
          alert('Herói excluído com sucesso!');
          this.obterHerois(); // Atualiza a lista de heróis
        },
        error: (err) => {
          console.error('Erro ao excluir herói:', err);
          alert('Não foi possível excluir o herói.');
        },
      });
    }
  }

  ngOnInit(): void {
    this.obterHerois();
  }
}
