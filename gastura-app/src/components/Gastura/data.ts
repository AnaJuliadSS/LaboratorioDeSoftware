// src/components/Gastura/data.ts
import { Categoria, Gasto, Meta, ModalidadePagamento } from './types';

export const categorias: Categoria[] = [
  { id: 1, nome: 'Mercado', cor: '#3b82f6' },
  { id: 2, nome: 'Lazer', cor: '#10b981' },
  { id: 3, nome: 'Contas', cor: '#f59e0b' }
];

export const metas: Meta[] = [
  { id: 1, categoriaId: 1, categoria: categorias[0], valor: 800, periodo: 'Junho 2025' },
  { id: 2, categoriaId: 2, categoria: categorias[1], valor: 400, periodo: 'Junho 2025' },
  { id: 3, categoriaId: 3, categoria: categorias[2], valor: 1200, periodo: 'Junho 2025' }
];

export const gastosMock: Gasto[] = [
  { 
    id: 1, 
    valor: 45.90, 
    dataHora: new Date('2025-06-10T12:30:00'), 
    descricao: 'Supermercado Extra', 
    modalidadePagamento: ModalidadePagamento.Credito, 
    usuarioId: 1, 
    categoriaId: 1, 
    categoria: categorias[0] 
  },
  { 
    id: 2, 
    valor: 25.50, 
    dataHora: new Date('2025-06-11T19:15:00'), 
    descricao: 'Cinema', 
    modalidadePagamento: ModalidadePagamento.Debito, 
    usuarioId: 1, 
    categoriaId: 2, 
    categoria: categorias[1] 
  },
  { 
    id: 3, 
    valor: 180.00, 
    dataHora: new Date('2025-06-12T08:00:00'), 
    descricao: 'Conta de Luz', 
    modalidadePagamento: ModalidadePagamento.Pix, 
    usuarioId: 1, 
    categoriaId: 3, 
    categoria: categorias[2] 
  },
  { 
    id: 4, 
    valor: 78.30, 
    dataHora: new Date('2025-06-09T14:20:00'), 
    descricao: 'Feira Livre', 
    modalidadePagamento: ModalidadePagamento.Dinheiro, 
    usuarioId: 1, 
    categoriaId: 1, 
    categoria: categorias[0] 
  },
  { 
    id: 5, 
    valor: 35.00, 
    dataHora: new Date('2025-06-08T20:45:00'), 
    descricao: 'Lanchonete', 
    modalidadePagamento: ModalidadePagamento.Pix, 
    usuarioId: 1, 
    categoriaId: 2, 
    categoria: categorias[1] 
  }
];