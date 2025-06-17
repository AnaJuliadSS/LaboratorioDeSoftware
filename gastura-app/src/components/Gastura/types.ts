// src/components/Gastura/types.ts

export enum ModalidadePagamento {
  Credito = 1,
  Debito = 2,
  Dinheiro = 3,
  Pix = 4
}

export interface Categoria {
  id: number;
  nome: string;
  cor: string;
}

export interface Gasto {
  id: number;
  valor: number;
  dataHora: Date;
  descricao: string;
  modalidadePagamento: ModalidadePagamento;
  usuarioId: number;
  categoriaId: number;
  categoria: Categoria;
}

export interface Meta {
  id: number;
  categoriaId: number;
  categoria: Categoria;
  valor: number;
  periodo: string;
}

export interface Filtros {
  categoria: string;
  modalidade: string;
  dataInicio: string;
  dataFim: string;
  valorMin: string;
  valorMax: string;
}