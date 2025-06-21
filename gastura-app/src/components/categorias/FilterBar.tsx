import React, { useState } from "react";
import Categoria from "../../types/Categoria";
import { api } from "../../services/api";

const FilterBar: React.FC<{
	categorias: Categoria[];
	onFilterChange: (filters: any) => void;
	onAddExpense: () => void;
	onAddCategoria: () => void;
	fetchCategorias: () => Promise<void>;
	fetchGastos: () => Promise<void>;
}> = ({ categorias, onFilterChange, onAddExpense, onAddCategoria, fetchCategorias, fetchGastos }) => {
	const [filters, setFilters] = useState({
		categoria: "",
		modalidadePagamento: "",
		dataInicio: "",
		dataFim: "",
		descricao: "",
	});

	const [showCategoriaList, setShowCategoriaList] = useState(false);

	const handleFilterChange = (key: string, value: string) => {
		const newFilters = { ...filters, [key]: value };
		setFilters(newFilters);
		onFilterChange(newFilters);
	};

	const handleDeleteCategoria = async (id: number, usuarioId: string) => {
		const confirm = window.confirm("Tem certeza que deseja excluir esta categoria?");
		if (!confirm) return;

		try {
			await api.delete(`/categorias/${id}?usuarioId=${usuarioId}`);
			await fetchCategorias();
			await fetchGastos();
		} catch (error) {
			console.error("Erro ao excluir categoria:", error);
			alert("Erro ao excluir categoria. Tente novamente.");
		}
	};

	return (
		<div className="card mb-4">
			<div className="card-body">
				<div className="row align-items-end">
					<div className="col-md-2 mb-2">
						<button className="btn btn-warning w-100" onClick={onAddExpense}>
							Novo gasto
						</button>
					</div>

					<div className="col-md-2 mb-2">
						<div className="d-flex align-items-center justify-content-between">
							<label className="form-label mb-0">Categoria</label>
							<div className="d-flex align-items-center gap-1">
								<button
									type="button"
									className="btn p-0 border-0"
									onClick={onAddCategoria}
									style={{ color: "#6cbaa4", background: "none" }}
									aria-label="Adicionar categoria"
								>
									<span className="material-icons" style={{ fontSize: "1.8rem" }}>
										add_circle
									</span>
								</button>

								<button
									type="button"
									className="btn p-0 border-0"
									onClick={() => setShowCategoriaList((prev) => !prev)}
									style={{ color: "#6cbaa4", background: "none" }}
									aria-label="Editar categorias"
								>
									<span className="material-icons" style={{ fontSize: "1.6rem" }}>
										edit
									</span>
								</button>
							</div>
						</div>
						<select
							className="form-select"
							value={filters.categoria}
							onChange={(e) => handleFilterChange("categoria", e.target.value)}
						>
							<option value="">Todas</option>
							{categorias.map((cat) => (
								<option key={cat.id} value={cat.id.toString()}>
									{cat.descricao}
								</option>
							))}
						</select>

						{showCategoriaList && (
							<div className="mt-2" style={{ maxHeight: "120px", overflowY: "auto" }}>
								{categorias.map((cat) => (
									<div
										key={cat.id}
										className="d-flex justify-content-between align-items-center mb-1"
									>
										<small className="text-muted">{cat.descricao}</small>
										<button
											className="btn btn-sm p-0 border-0"
											onClick={() => handleDeleteCategoria(parseInt(cat.id), cat.usuarioId)}
											style={{ color: "#dc3545", background: "none" }}
											title="Excluir categoria"
										>
											<span className="material-icons" style={{ fontSize: "18px" }}>
												remove_circle
											</span>
										</button>
									</div>
								))}
							</div>
						)}
						
					</div>

					<div className="col-md-2 mb-2">
						<label className="form-label">Pagamento</label>
						<select
							className="form-select"
							value={filters.modalidadePagamento}
							onChange={(e) => handleFilterChange("modalidadePagamento", e.target.value)}
						>
							<option value="">Todos</option>
							<option value="1">Crédito</option>
							<option value="2">Débito</option>
							<option value="3">Dinheiro</option>
							<option value="4">PIX</option>
						</select>
					</div>

					<div className="col-md-2 mb-2">
						<label className="form-label">Data Início</label>
						<input
							type="date"
							className="form-control"
							value={filters.dataInicio}
							onChange={(e) => handleFilterChange("dataInicio", e.target.value)}
						/>
					</div>

					<div className="col-md-2 mb-2">
						<label className="form-label">Data Fim</label>
						<input
							type="date"
							className="form-control"
							value={filters.dataFim}
							onChange={(e) => handleFilterChange("dataFim", e.target.value)}
						/>
					</div>

					<div className="col-md-2 mb-2">
						<label className="form-label">Descrição</label>
						<input
							type="text"
							className="form-control"
							placeholder="Buscar..."
							value={filters.descricao}
							onChange={(e) => handleFilterChange("descricao", e.target.value)}
						/>
					</div>
				</div>
			</div>
		</div>
	);
};

export default FilterBar;
