import { Pessoa } from './pessoa-model';
import { Pagamento } from './pagamento-model';
import { Aluno } from './aluno-model';

export class MatriculaAluno {
  matriculaAlunoId: string;
  alunoId: string;
  situacaoMatriculaId: number;
  ativo: boolean;
  criacaoDate: Date;
  cancelamentoDate: Date;
  alteracaoDate: Date;
  aluno: Aluno;
}



