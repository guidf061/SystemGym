import { Pessoa } from './pessoa-model';

export class Colaborador {
  colaboradorId: string;
  funcaoId: number;
  situacaoColaboradorId: number;
  numeroSerieCtps: string;
  numeroCtps: string;
  numeroPisPasep: string;
  docIdentidade: string;
  criacaoData: Date;
  alteracaoData: Date;
  pessoa: Pessoa;
}
