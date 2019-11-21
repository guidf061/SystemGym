
import { Aluno } from './aluno-model';

export class Pagamento {
  pagamentoId: string;
  alunoId: string;
  colaboradorId: string;
  planoId: number;
  valorMensalidade: string;
  mesId: number;
  anoId: number;
  formaPagamentoId: number;
  pagamentoDate: Date;
  criacaoDate: Date;
  aluno: Aluno;
}



