import ModalidadePagamento from "./ModalidadePagamento";

interface CreateGastoDTO {
  valor: number;
  modalidadePagamento: ModalidadePagamento;
  usuarioId: number;
  categoriaId: number;
  descricao: string;
  dataHora?: Date | null;
}

export default CreateGastoDTO;