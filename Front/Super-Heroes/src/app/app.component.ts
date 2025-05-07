import { Hero } from './models/hero';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HeroService } from './services/hero.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  constructor(private heroService: HeroService) {}

  title = 'Super-Heroes';
  http = inject(HttpClient);

  //listar herois
  heroes: Hero[] = [];
  carregando: boolean = true;

  //buscar herói
  heroesFiltrados: Hero[] = [];
  filtroBusca: string = '';

  //adicionar heroi
  nomeAdicionar = '';
  nomeHeroiAdicionar = '';
  superpoderesAdicionar = '';
  nascimentoAdicionar = '';
  alturaAdicionar = 0;
  pesoAdicionar = 0;

  //editar heroi
  heroiSelecionado: Hero = {
    id: 0,
    nome: '',
    nomeHeroi: '',
    superpoderes: '',
    dataNascimento: '',
    altura: 0,
    peso: 0,
  };

  //MÉTODOS

  //lista heróis
  obterHerois() {
    this.carregando = true;
    this.heroService.obterHerois().subscribe((heroes) => {
      this.heroes = heroes;
      this.heroesFiltrados = heroes;
      this.carregando = false;
    });
  }

  //pesquisa herói por nome ou id
  filtrarHerois() {
    const filtro = this.filtroBusca.toLowerCase();

    this.heroesFiltrados = this.heroes.filter((hero) => {
      return (
        hero.id.toString().includes(filtro) ||
        hero.nomeHeroi.toLowerCase().includes(filtro)
      );
    });
  }

  //adiciona novo heroi
  adicionarHeroi() {
    if (!this.nomeAdicionar) {
      alert('O nome do herói é obrigatório!');
      return;
    }

    // Verifica se há duplicidade de nomes de heróis
    const nomeDuplicado = this.heroes.some(
      (hero) =>
        hero.nomeHeroi.toLowerCase() === this.nomeHeroiAdicionar.toLowerCase()
    );

    if (nomeDuplicado) {
      alert('Já existe um herói com este nome. Por favor, escolha outro nome.');
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
        this.obterHerois(); // Atualiza a lista de heróis
        this.limparCampos(); // Limpa os campos do formulário
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
    this.alturaAdicionar = 0;
    this.pesoAdicionar = 0;
  }

  //edita heroi
  abrirModalEdicao(heroi: Hero) {
    this.heroiSelecionado = { ...heroi };
  }

  salvarAlteracoesHeroi() {
    // Verifica se já existe herói com o mesmo nome
    const nomeDuplicado = this.heroes.some(
      (hero) =>
        hero.nomeHeroi.toLowerCase() ===
          this.heroiSelecionado.nomeHeroi.toLowerCase() &&
        hero.id !== this.heroiSelecionado.id // Ignora o herói que está sendo editado
    );

    if (nomeDuplicado) {
      alert(
        'Já existe outro herói com este nome. Por favor, escolha outro nome.'
      );
      return;
    }

    // Chama o serviço para salvar as alterações
    this.heroService
      .editarHeroi(this.heroiSelecionado.id, this.heroiSelecionado)
      .subscribe({
        next: () => {
          alert('Herói atualizado com sucesso!');
          this.obterHerois();
        },
        error: (err) => {
          console.error('Erro ao salvar alterações:', err);
          alert('Não foi possível salvar as alterações.');
        },
      });
  }

  //exclui heroi
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
