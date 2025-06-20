import { useEffect, useState } from "react";
import ModalidadePagamento from "../../types/ModalidadePagamento";
import Gasto from "../../types/Gasto";
import Categoria from "../../types/Categoria";
import CreateGastoDTO from "../../types/CreateGastoDTO";

const ExpenseModal: React.FC<{
	show: boolean;
	onHide: () => void;
	gasto?: Gasto | null;
	categorias: Categoria[];
	onSave: (gasto: CreateGastoDTO) => void;
}> = ({ show, onHide, gasto, categorias, onSave }) => {
	const [formData, setFormData] = useState({
		valor: "",
		dataHora: "",
		descricao: "",
		modalidadePagamento: ModalidadePagamento.Credito,
		categoriaId: "",
	});

	useEffect(() => {
		if (gasto) {
			setFormData({
				valor: gasto.valor.toString(),
				dataHora: gasto.dataHora ? new Date(gasto.dataHora).toISOString().slice(0, 16) : "",
				descricao: gasto.descricao,
				modalidadePagamento: gasto.modalidadePagamento,
				categoriaId: gasto.categoriaId,
			});
		} else {
			setFormData({
				valor: "",
				dataHora: formData.dataHora
					? new Date(formData.dataHora).toString()
					: new Date().toString(),
				descricao: "",
				modalidadePagamento: ModalidadePagamento.Credito,
				categoriaId: "",
			});
		}
	}, [gasto, show]);

	const handleSubmit = () => {
		if (!formData.descricao || !formData.valor || !formData.categoriaId) {
			alert("Por favor, preencha todos os campos obrigatórios.");
			return;
		}

		const gastoData: CreateGastoDTO = {
			valor: parseFloat(formData.valor),
			dataHora: formData.dataHora ? new Date(formData.dataHora) : null,
			descricao: formData.descricao,
			modalidadePagamento: formData.modalidadePagamento,
			usuarioId: 0, // será preenchido no componente pai
			categoriaId: parseInt(formData.categoriaId),
		};

		onSave(gastoData);
		onHide();
	};

	if (!show) return null;

	return (
		<div className="modal show d-block" style={{ backgroundColor: "rgba(0,0,0,0.5)" }}>
			<div className="modal-dialog">
				<div className="modal-content">
					<div className="modal-header">
						<h5 className="modal-title">{gasto ? "Editar Gasto" : "Adicionar Gasto"}</h5>
						<button type="button" className="btn-close" onClick={onHide}></button>
					</div>
					<div className="modal-body">
						<div className="mb-3">
							<label className="form-label">Descrição</label>
							<input
								type="text"
								className="form-control"
								value={formData.descricao}
								onChange={(e) => setFormData({ ...formData, descricao: e.target.value })}
							/>
						</div>

						<div className="mb-3">
							<label className="form-label">Valor (R$)</label>
							<input
								type="number"
								step="0.01"
								className="form-control"
								value={formData.valor}
								onChange={(e) => setFormData({ ...formData, valor: e.target.value })}
							/>
						</div>

						<div className="mb-3">
							<label className="form-label">Categoria</label>
							<select
								className="form-select"
								value={formData.categoriaId}
								onChange={(e) => setFormData({ ...formData, categoriaId: e.target.value })}
							>
								{categorias.map((cat) => (
									<option key={cat.id} value={cat.id}>
										{cat.descricao}
									</option>
								))}
							</select>
						</div>

						<div className="mb-3">
							<label className="form-label">Modalidade de Pagamento</label>
							<select
								className="form-select"
								value={formData.modalidadePagamento}
								onChange={(e) =>
									setFormData({
										...formData,
										modalidadePagamento: parseInt(e.target.value) as ModalidadePagamento,
									})
								}
							>
								<option value={ModalidadePagamento.Credito}>Crédito</option>
								<option value={ModalidadePagamento.Debito}>Débito</option>
								<option value={ModalidadePagamento.Dinheiro}>Dinheiro</option>
								<option value={ModalidadePagamento.Pix}>PIX</option>
							</select>
						</div>

						<div className="mb-3">
							<label className="form-label">Data e Hora</label>
							<input
								type="datetime-local"
								className="form-control"
								value={formData.dataHora}
								onChange={(e) => setFormData({ ...formData, dataHora: e.target.value })}
							/>
						</div>
					</div>
					<div className="modal-footer">
						<button type="button" className="btn btn-secondary" onClick={onHide}>
							Cancelar
						</button>
						<button type="button" className="btn btn-primary" onClick={handleSubmit}>
							{gasto ? "Salvar Alterações" : "Adicionar Gasto"}
						</button>
					</div>
				</div>
			</div>
		</div>
	);
};

export default ExpenseModal;
