import { Pessoa } from './pessoa-model';

export class Usuario {
  usuarioId: string;
  userName: string;
  password: string;
  dataAcesso: Date;
  dataCadastro: Date;
  dataAlteracao: Date;
  pessoa: Pessoa;
}
