import { Pessoa } from './pessoa-model';

export class Aluno {
  
  alunoId: string;
  pessoaId: string;
  numeroCartao: string;
  numeroWhatsapp: string;
  situacaoMatriculaId: number;
  criacaoData: Date;
  alteracaoData: Date;     
  pessoa: Pessoa;
  matriculaAlunoId: string;
  ativo: boolean;
  cancelamentoDate: Date;

}



