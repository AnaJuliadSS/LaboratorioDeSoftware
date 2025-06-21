import Categoria from "./Categoria";
import ModalidadePagamento from "./ModalidadePagamento";
import Usuario from "./Usuario";

interface Gasto {
  id: string;
  valor: number;
  dataHora?: Date;
  descricao: string;
  modalidadePagamento: ModalidadePagamento;
  usuarioId: string;
  categoriaId: string;
  categoria: Categoria | null;
  usuario: Usuario;
}

export default Gasto;