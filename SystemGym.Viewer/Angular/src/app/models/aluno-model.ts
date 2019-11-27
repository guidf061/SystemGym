import { Pessoa } from './pessoa-model';
import { Pagamento } from './pagamento-model';

export class Aluno {
  alunoId: string;
  pessoaId: string;
  numeroCartao: string;
  numeroWhatsapp: string;
  criacaoDate: Date;
  alteracaoData: Date;     
  pessoa: Pessoa;
  pagamento: Pagamento[];
}



