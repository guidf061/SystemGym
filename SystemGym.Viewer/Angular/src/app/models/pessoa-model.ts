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
  dataNascimento: Date;
  criacaoData: Date;
  alteracaoData: Date;
  stateId: number;
  cityId: number;
  city: City;
}
