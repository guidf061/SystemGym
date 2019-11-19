import { City } from './city-model';
import { Sexo } from './sexo-model';

export class Pessoa {
  pessoaId: string;
  nome: string;
  email: string;
  cpf: string;
  sexoId: number;
  endereco: string;
  permissaoId: number;
  telefoneCelular: string;
  telefoneCasa: string;
  criacaoData: Date;
  alteracaoData: Date;
  dataNascimento: Date;
  stateId: number;
  cityId: number;
  city: City;
}
