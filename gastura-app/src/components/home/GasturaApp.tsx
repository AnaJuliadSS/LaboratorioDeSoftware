import React, { useState, useEffect } from "react";
import Categoria from "../../types/Categoria";
import Usuario from "../../types/Usuario";
import Gasto from "../../types/Gasto";
import FilterBar from "../categorias/FilterBar";
import ExpenseTable from "../gastos/ExpenseTable";
import StatsCards from "../metas/StatsCards";
import ExpenseModal from "../gastos/ExpenseModal";
import "./GasturaApp.css";
import { api } from "../../services/api";
import Orcamento from "../../types/Orcamento";
import CreateGastoDTO from "../../types/CreateGastoDTO";
import CategoriaModal from "../categorias/CategoriaModal";

// Mock Data
const mockUsuario: Usuario = {
	id: "2",
	nome: "Aninha",
	email: "user@example.com",
	dataCadastro: new Date("2025-05-12T22:52:01.634449"),
};

// Main App Component
const GasturaApp: React.FC = () => {
	const [categorias, setCategorias] = useState<Categoria[]>([]);
	const [gastos, setGastos] = useState<Gasto[]>([]);
	const [orcamentos, setOrcamentos] = useState<Orcamento[]>([]);
	const [filteredGastos, setFilteredGastos] = useState<Gasto[]>([]);
	const [showModal, setShowModal] = useState(false);
	const [editingGasto, setEditingGasto] = useState<Gasto | null>(null);
	const [showCategoriaModal, setShowCategoriaModal] = useState(false);

	const fetchGastos = async () => {
		try {
			const response = await api.get(`/gastos/${mockUsuario.id}`);
			setGastos(response.data);
			setFilteredGastos(response.data);
		} catch (error) {
			console.error("Erro ao buscar gastos:", error);
		}
	};

	const fetchOrcamentos = async () => {
		try {
			const response = await api.get(`/orcamentos/${mockUsuario.id}`);
			setOrcamentos(response.data);
		} catch (error) {
			console.error("Erro ao buscar orcamentos:", error);
		}
	};

	const fetchCategorias = async () => {
		try {
			const response = await api.get(`categorias/${mockUsuario.id}`);
			setCategorias(response.data);
		} catch (error) {
			console.error("Erro ao buscar categorias:", error);
		}
	};

	useEffect(() => {
		fetchOrcamentos();
		fetchCategorias();
		fetchGastos();
	}, []);

	// Load Bootstrap CSS
	useEffect(() => {
		const link = document.createElement("link");
		link.rel = "stylesheet";
		link.href = "https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.0/css/bootstrap.min.css";
		document.head.appendChild(link);

		return () => {
			document.head.removeChild(link);
		};
	}, []);

	const handleSaveCategoria = async (novaCategoria: { descricao: string; cor: string }) => {
		try {
			let usuarioId = categorias[0].usuarioId;
			await api.post("/categorias", { ...novaCategoria, usuarioId });
			fetchCategorias(); // recarrega categorias
		} catch (error) {
			console.error("Erro ao adicionar categoria", error);
			alert("Erro ao adicionar categoria");
		}
	};

	const handleFilterChange = (filters: any) => {
		let filtered = gastos;

		if (filters.categoria) {
			filtered = filtered.filter((g) => g.categoriaId.toString() === filters.categoria);
		}

		if (filters.modalidadePagamento) {
			filtered = filtered.filter(
				(g) => g.modalidadePagamento.toString() === filters.modalidadePagamento
			);
		}

		if (filters.dataInicio) {
			const dataInicio = new Date(filters.dataInicio);
			filtered = filtered.filter((g) => g.dataHora && new Date(g.dataHora) >= dataInicio);
		}

		if (filters.dataFim) {
			const dataFim = new Date(filters.dataFim);
			dataFim.setHours(23, 59, 59, 999);
			filtered = filtered.filter((g) => g.dataHora && new Date(g.dataHora) <= dataFim);
		}

		if (filters.descricao) {
			filtered = filtered.filter((g) =>
				g.descricao.toLowerCase().includes(filters.descricao.toLowerCase())
			);
		}

		setFilteredGastos(filtered);
	};

	const handleAddExpense = () => {
		setEditingGasto(null);
		setShowModal(true);
	};

	const handleEditExpense = (gasto: Gasto) => {
		setEditingGasto(gasto);
		setShowModal(true);
	};

	function onAddCategoria() {
		setShowCategoriaModal(true);
	}

	const handleSaveExpense = async (gastoData: CreateGastoDTO) => {
		try {
			if (editingGasto) {
				// PUT para editar (ajuste se sua API usa PATCH ou outro)
				await api.put(`/gastos/${editingGasto.id}`, {
					...gastoData,
					usuarioId: mockUsuario.id,
				});
			} else {
				// POST para adicionar
				await api.post("/gastos", {
					...gastoData,
					usuarioId: mockUsuario.id,
				});
			}

			// Recarregar os dados após salvar
			await fetchGastos();
			setShowModal(false);
		} catch (error) {
			console.error("Erro ao salvar gasto:", error);
		}
	};

	const handleDeleteExpense = async (id: string) => {
		if (!window.confirm("Tem certeza que deseja excluir este gasto?")) return;

		try {
			// Exclui no backend
			await api.delete(`/gastos/${id}?usuarioId=${mockUsuario.id}`);

			// Atualiza o estado local
			const updatedGastos = gastos.filter((g) => g.id !== id);
			setGastos(updatedGastos);
			setFilteredGastos(updatedGastos);
		} catch (error) {
			console.error("Erro ao excluir gasto:", error);
			alert("Ocorreu um erro ao tentar excluir o gasto. Tente novamente.");
		}
	};

	return (
		<div className="space-y-6">
			<div className="container-fluid py-4">
				<div className="row">
					<div className="col-12">
						<h1 className="mb-4">Gastura</h1>

						<StatsCards
							gastos={gastos}
							orcamentos={orcamentos}
							categorias={categorias}
							usuarioId={parseInt(mockUsuario.id)}
							fetchOrcamentos={fetchOrcamentos}
						/>

						<FilterBar
							categorias={categorias}
							onFilterChange={handleFilterChange}
							onAddExpense={handleAddExpense}
							onAddCategoria={onAddCategoria}
							fetchCategorias={fetchCategorias}
							fetchGastos={fetchGastos}
						/>

						<ExpenseTable
							gastos={filteredGastos}
							onEditExpense={handleEditExpense}
							onDeleteExpense={handleDeleteExpense}
						/>

						<ExpenseModal
							show={showModal}
							onHide={() => setShowModal(false)}
							gasto={editingGasto}
							categorias={categorias}
							onSave={handleSaveExpense}
						/>

						<CategoriaModal
							show={showCategoriaModal}
							onHide={() => setShowCategoriaModal(false)}
							onSave={handleSaveCategoria}
						/>
					</div>
				</div>
			</div>
		</div>
	);
};

export default GasturaApp;
