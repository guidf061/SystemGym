import { Pessoa } from './pessoa-model';

export class Visitante {
  visitanteId: string;
  docIdentidade: string;
  visitaData: Date;
  criacaoData: Date;
  alteracaoData: Date;
  pessoa: Pessoa;
}
