import Categoria from "./Categoria";

interface Orcamento {
  id: string;
  usuarioid: string;
  categoriaId: string;
  valorLimite: number;
  mesReferencia: Date;
  categoria: Categoria;
}

export default Orcamento;

