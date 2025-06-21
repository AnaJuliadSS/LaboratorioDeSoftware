import { useEffect, useState } from "react";
import Orcamento from "../../types/Orcamento";
import Categoria from "../../types/Categoria";
import CreateOrcamentoDTO from "../../types/CreateOrcamentoDTO.ts";
import "react-datepicker/dist/react-datepicker.css";
import { registerLocale } from "react-datepicker";
import DatePicker from "react-datepicker";

import { ptBR } from "date-fns/locale";

// Registro do idioma
registerLocale("pt-BR", ptBR);

const BudgetModal: React.FC<{
	show: boolean;
	onHide: () => void;
	orcamento?: Orcamento | null;
	categorias: Categoria[];
	orcamentos: Orcamento[];
	onSave: (orcamento: CreateOrcamentoDTO) => void;
}> = ({ show, onHide, orcamento, categorias, onSave, orcamentos }) => {
	const [formData, setFormData] = useState({
		valorLimite: "",
		mesReferencia: "",
		categoriaId: "",
	});

	// Converter mês de referência do form para comparável
	const mesSelecionado = formData.mesReferencia;

	// Filtrar categorias que já têm orçamento para o mesmo mês
	const categoriasDisponiveis = categorias.filter((categoria) => {
		const jaTemOrcamento = orcamentos.some((orc) => {
			const mesRef = new Date(orc.mesReferencia).toISOString().slice(0, 7);
			return (
				mesRef === mesSelecionado &&
				orc.categoriaId === categoria.id &&
				// se estamos editando, permitir exibir a categoria que já está no orçamento atual
				orcamento?.categoriaId !== categoria.id
			);
		});
		return !jaTemOrcamento;
	});

	useEffect(() => {
		if (orcamento) {
			// Converter Date para formato YYYY-MM
			const mesRef = orcamento.mesReferencia
				? new Date(orcamento.mesReferencia).toISOString().slice(0, 7)
				: "";

			setFormData({
				valorLimite: orcamento.valorLimite.toString(),
				mesReferencia: mesRef,
				categoriaId: orcamento.categoriaId,
			});
		} else {
			// Para novo orçamento, limpar campos
			setFormData({
				valorLimite: "",
				mesReferencia: "",
				categoriaId: "",
			});
		}
	}, [orcamento, show]);

	const handleSubmit = () => {
		if (!formData.valorLimite || !formData.mesReferencia || !formData.categoriaId) {
			alert("Por favor, preencha todos os campos obrigatórios.");
			return;
		}

		// Converter YYYY-MM para Date (dia 1 do mês)
		const [ano, mes] = formData.mesReferencia.split("-");
		const mesReferenciaDate = new Date(parseInt(ano), parseInt(mes) - 1, 1);

		const orcamentoData: CreateOrcamentoDTO = {
			valorLimite: parseFloat(formData.valorLimite),
			mesReferencia: mesReferenciaDate,
			usuarioId: 0, // será preenchido no componente pai
			categoriaId: parseInt(formData.categoriaId),
		};

		onSave(orcamentoData);
		onHide();
	};

	if (!show) return null;

	return (
		<div className="modal show d-block" style={{ backgroundColor: "rgba(0,0,0,0.5)" }}>
			<div className="modal-dialog">
				<div className="modal-content">
					<div className="modal-header">
						<h5 className="modal-title">
							{orcamento ? "Editar Orçamento" : "Adicionar Orçamento"}
						</h5>
						<button type="button" className="btn-close" onClick={onHide}></button>
					</div>
					<div className="modal-body">
						<div className="mb-3">
							<label className="form-label">Valor Limite (R$)</label>
							<input
								type="number"
								step="0.01"
								className="form-control"
								value={formData.valorLimite}
								onChange={(e) => setFormData({ ...formData, valorLimite: e.target.value })}
							/>
						</div>

						<div className="mb-3">
							<label className="form-label">Categoria</label>
							<select
								className="form-select"
								value={formData.categoriaId}
								onChange={(e) => setFormData({ ...formData, categoriaId: e.target.value })}
							>
								<option value="">Escolha uma categoria</option>
								{categoriasDisponiveis.map((cat) => (
									<option key={cat.id} value={cat.id}>
										{cat.descricao}
									</option>
								))}
							</select>
						</div>

						<div className="mb-3">
							<label className="form-label">Mês de Referência</label>
							<DatePicker
								selected={formData.mesReferencia ? new Date(formData.mesReferencia + "-01") : null}
								onChange={(date) => {
									if (date) {
										const year = date.getFullYear();
										const month = String(date.getMonth() + 1).padStart(2, "0");
										setFormData({ ...formData, mesReferencia: `${year}-${month}` });
									}
								}}
								dateFormat="MM/yyyy"
								showMonthYearPicker
								locale="pt-BR"
								className="form-control"
								placeholderText="Selecione o mês"
							/>
						</div>
					</div>
					<div className="modal-footer">
						<button type="button" className="btn btn-secondary" onClick={onHide}>
							Cancelar
						</button>
						<button type="button" className="btn btn-primary" onClick={handleSubmit}>
							{orcamento ? "Salvar Alterações" : "Adicionar Orçamento"}
						</button>
					</div>
				</div>
			</div>
		</div>
	);
};

export default BudgetModal;
