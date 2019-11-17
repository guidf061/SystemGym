import { Pessoa } from './pessoa-model';

export class Aluno {
  alunoId: string;
  numeroCartao: string;
  numeroWhatsapp: string;
  criacaoData: Date;
  alteracaoData: Date;     
  pessoa: Pessoa; 
}
