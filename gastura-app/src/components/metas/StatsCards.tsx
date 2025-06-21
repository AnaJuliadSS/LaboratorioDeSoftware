import { useState } from "react";
import Categoria from "../../types/Categoria";
import Gasto from "../../types/Gasto";
import Orcamento from "../../types/Orcamento";
import BudgetModal from "./BudgetModal";
import CreateOrcamentoDTO from "../../types/CreateOrcamentoDTO";
import { api } from "../../services/api";

const formatCurrency = (value: number): string => {
	return new Intl.NumberFormat("pt-BR", {
		style: "currency",
		currency: "BRL",
	}).format(value);
};

const getRandomColor = () => {
	const letters = "0123456789ABCDEF";
	let color = "#";
	for (let i = 0; i < 6; i++) {
		color += letters[Math.floor(Math.random() * 16)];
	}
	return color;
};

interface StatsCardsProps {
	gastos: Gasto[];
	orcamentos: Orcamento[];
	categorias: Categoria[];
	usuarioId: number;
	fetchOrcamentos: () => Promise<void>;
}

const StatsCards: React.FC<StatsCardsProps> = ({
	gastos,
	orcamentos,
	categorias,
	usuarioId,
	fetchOrcamentos,
}) => {
	const currentMonth = new Date().getMonth();
	const currentYear = new Date().getFullYear();
	const [showModal, setShowModal] = useState(false);
	const [editingOrcamento, setEditingOrcamento] = useState<Orcamento | null>(null);

	const handleDeleteOrcamento = async (id: string) => {
		if (!window.confirm("Tem certeza que deseja excluir este orcamento?")) return;

		try {
			// Exclui no backend
			await api.delete(`/orcamentos/${id}?usuarioId=${usuarioId}`);

			// Atualiza o estado local
			fetchOrcamentos();
		} catch (error) {
			console.error("Erro ao excluir orcamento:", error);
			alert("Ocorreu um erro ao tentar excluir o orçamento. Tente novamente.");
		}
	};

	const handleSaveBudget = async (orcamentoData: CreateOrcamentoDTO) => {
		try {
			if (editingOrcamento) {
				// PUT para editar (ajuste se sua API usa PATCH ou outro)
				await api.put(`/orcamentos/${editingOrcamento.id}`, {
					...orcamentoData,
					usuarioId: usuarioId,
				});
			} else {
				// POST para adicionar
				await api.post("/orcamentos", {
					...orcamentoData,
					usuarioId: usuarioId,
				});
			}

			// Recarregar os dados após salvar
			await fetchOrcamentos();
			setShowModal(false);
		} catch (error) {
			console.error("Erro ao salvar orcamento:", error);
		}
	};

	// Filtrar gastos do mês atual
	const gastosDoMes = gastos.filter((g) => {
		const gastoDate = g.dataHora ? new Date(g.dataHora) : new Date();
		return gastoDate.getMonth() === currentMonth && gastoDate.getFullYear() === currentYear;
	});

	const totalGasto = gastosDoMes.reduce((sum, g) => sum + g.valor, 0);

	// Filtrar orçamentos do mês atual
	const orcamentosDoMes = orcamentos.filter((o) => {
		const mesRef = new Date(o.mesReferencia);
		return mesRef.getMonth() === currentMonth && mesRef.getFullYear() === currentYear;
	});

	// Calcular estatísticas por orçamento
	const statsPorOrcamento = orcamentosDoMes.map((orc) => {
		// Filtrar gastos desta categoria no mês atual
		const gastosCategoria = gastosDoMes.filter((g) => g.categoriaId === orc.categoriaId);
		const total = gastosCategoria.reduce((sum, g) => sum + g.valor, 0);
		const percentual = orc.valorLimite > 0 ? (total / orc.valorLimite) * 100 : 0;

		return {
			orcamento: orc,
			categoria: orc.categoria,
			total,
			percentual,
			isProximoLimite: percentual >= 80 && percentual <= 100,
			isLimiteAtingido: percentual == 100,
			isAcimaLimite: percentual > 100,
		};
	});

	const todasStats = [...statsPorOrcamento];

	return (
		<div className="row mb-4">
			<div className="col-12 mb-3">
				<div className="card">
					<div className="card-body">
						<h5 className="card-title">Total Gasto este Mês</h5>
						<h3 className="text-primary">{formatCurrency(totalGasto)}</h3>
						<small className="text-muted">
							{new Date().toLocaleDateString("pt-BR", { month: "long", year: "numeric" })}
						</small>
					</div>
				</div>
				<div className="col-md-2 mb-2">
					<button
						className="btn btn-warning w-100"
						onClick={() => {
							setEditingOrcamento(null);
							setShowModal(true);
						}}
					>
						Novo orçamento
					</button>
				</div>
			</div>
			<BudgetModal
				show={showModal}
				onHide={() => setShowModal(false)}
				orcamento={editingOrcamento}
				categorias={categorias}
				onSave={handleSaveBudget}
				orcamentos={orcamentos}
			/>
			{todasStats.length > 0 ? (
				todasStats.map(
					({
						orcamento,
						categoria,
						total,
						percentual,
						isProximoLimite,
						isAcimaLimite,
						isLimiteAtingido,
					}) => (
						<div key={categoria?.id || "unknown"} className="col-md-6 col-lg-4 mb-3">
							<div
								className={`card h-100 ${
									isAcimaLimite
										? "border-danger"
										: isProximoLimite
										? "border-warning"
										: "border-0 shadow-sm"
								}`}
							>
								<div className="card-body">
									<div className="d-flex justify-content-between align-items-center mb-2">
										<h6
											className="card-title mb-0"
											style={{ color: categoria.cor ? categoria.cor : getRandomColor() }}
										>
											{categoria?.descricao || "Categoria não definida"}
										</h6>
										<div className="d-flex gap-2">
											<button
												className="btn btn-sm btn-outline-success"
												onClick={() => {
													setEditingOrcamento(orcamento);
													setShowModal(true);
												}}
											>
												Editar
											</button>

											{orcamento && (
												<button
													className="btn btn-sm btn-outline-danger"
													onClick={() => handleDeleteOrcamento(orcamento.id)}
												>
													Excluir
												</button>
											)}
										</div>
										{(isProximoLimite || isAcimaLimite) && (
											<span
												className={`badge ${
													isAcimaLimite
														? "bg-danger"
														: isLimiteAtingido
														? "bg-warning"
														: "bg-warning"
												}`}
											>
												{isAcimaLimite
													? "Limite excedido!"
													: isLimiteAtingido
													? "Limite atingido!"
													: "Próximo ao limite!"}
											</span>
										)}
									</div>
									<p className="card-text mb-1">
										<strong>{formatCurrency(total)}</strong>
										{orcamento && (
											<span className="text-muted"> / {formatCurrency(orcamento.valorLimite)}</span>
										)}
									</p>

									{orcamento ? (
										<div className="progress" style={{ height: "6px" }}>
											<div
												className={`progress-bar ${
													isAcimaLimite
														? "bg-danger"
														: isProximoLimite
														? "bg-warning"
														: "bg-success"
												}`}
												style={{ width: `${Math.min(percentual, 100)}%` }}
											></div>
										</div>
									) : (
										<small className="text-info">
											<i className="fas fa-info-circle me-1"></i>
											Sem orçamento definido
										</small>
									)}
								</div>
							</div>
						</div>
					)
				)
			) : (
				<div className="col-12">
					<div className="card border-0 shadow-sm">
						<div className="card-body text-center py-5">
							<i className="fas fa-chart-line fa-3x text-muted mb-3"></i>
							<h5 className="text-muted">Nenhum gasto registrado este mês</h5>
							<p className="text-muted mb-0">Comece adicionando seus primeiros gastos!</p>
						</div>
					</div>
				</div>
			)}
		</div>
	);
};

export default StatsCards;
