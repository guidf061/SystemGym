import { Pessoa } from './pessoa-model';

export class Colaborador {
  colaboradorId: string;
  funcaoId: number;
  situacaoColaboradorId?: number;
  criacaoData?: Date;
  alteracaoData?: Date;
  pessoa: Pessoa;
}
