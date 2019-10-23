import { Pessoa } from './pessoa-model';

export class Aluno {
  alunoId: string;
  numeroCartao: string; 
  situacaoAlunoId?: number;
  criacaoData: Date;
  alteracaoData: Date;     
  pessoa: Pessoa; 
}
