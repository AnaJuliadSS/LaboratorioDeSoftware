import Usuario from "./Usuario";

interface Categoria {
  id: string;
  descricao: string;
  usuarioId: string;
  usuario: Usuario | null;
  cor: string;
}

export default Categoria;