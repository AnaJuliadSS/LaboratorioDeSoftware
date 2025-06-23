import { useState } from "react";
import Gasto from "../../types/Gasto";
import ModalidadePagamento from "../../types/ModalidadePagamento";
import { ArrowDown, ArrowUp, ArrowUpDown } from "lucide-react";

const formatCurrency = (value: number): string => {
	return new Intl.NumberFormat("pt-BR", {
		style: "currency",
		currency: "BRL",
	}).format(value);
};

const formatDateTime = (date: Date): string => {
	return date.toLocaleString("pt-BR");
};

const getRandomColor = () => {
	const letters = "0123456789ABCDEF";
	let color = "#";
	for (let i = 0; i < 6; i++) {
		color += letters[Math.floor(Math.random() * 16)];
	}
	return color;
};

const getModalidadePagamentoText = (modalidade: ModalidadePagamento): string => {
	switch (modalidade) {
		case ModalidadePagamento.Credito:
			return "Crédito";
		case ModalidadePagamento.Debito:
			return "Débito";
		case ModalidadePagamento.Dinheiro:
			return "Dinheiro";
		case ModalidadePagamento.Pix:
			return "PIX";
		default:
			return "";
	}
};

const ExpenseTable: React.FC<{
	gastos: Gasto[];
	onEditExpense: (gasto: Gasto) => void;
	onDeleteExpense: (id: string) => void;
}> = ({ gastos, onEditExpense, onDeleteExpense }) => {
	const [sortField, setSortField] = useState<string>("dataHora");
	const [sortDirection, setSortDirection] = useState<"asc" | "desc">("desc");

	const handleSort = (field: string) => {
		if (sortField === field) {
			setSortDirection(sortDirection === "asc" ? "desc" : "asc");
		} else {
			setSortField(field);
			setSortDirection("asc");
		}
	};

	const sortedGastos = [...gastos].sort((a, b) => {
		let aValue: any, bValue: any;

		switch (sortField) {
			case "valor":
				aValue = a.valor;
				bValue = b.valor;
				break;
			case "dataHora":
				aValue = a.dataHora ? new Date(a.dataHora).getTime() : 0;
				bValue = b.dataHora ? new Date(b.dataHora).getTime() : 0;
				break;
			case "categoria":
				aValue = a.categoria?.descricao ?? ""; // Se não tiver categoria, considera string vazia
				bValue = b.categoria?.descricao ?? "";
				break;
			case "descricao":
				aValue = a.descricao;
				bValue = b.descricao;
				break;
			default:
				return 0;
		}

		if (aValue < bValue) return sortDirection === "asc" ? -1 : 1;
		if (aValue > bValue) return sortDirection === "asc" ? 1 : -1;
		return 0;
	});

	const getSortIcon = (field: string) => {
		if (sortField !== field) return <ArrowUpDown size={16} className="ml-1 text-gray-500" />;
		return sortDirection === "asc" ? (
			<ArrowUp size={16} className="ml-1 text-blue-600" />
		) : (
			<ArrowDown size={16} className="ml-1 text-blue-600" />
		);
	};
	return (
		<div className="card">
			<div className="card-body">
				<div className="table-responsive">
					<table className="table table-hover">
						<thead>
							<tr>
								<th
									scope="col"
									style={{ cursor: "pointer" }}
									onClick={() => handleSort("descricao")}
								>
									Descrição {getSortIcon("descricao")}
								</th>
								<th scope="col">Categoria</th>
								<th scope="col" style={{ cursor: "pointer" }} onClick={() => handleSort("valor")}>
									Valor {getSortIcon("valor")}
								</th>
								<th scope="col">Modalidade Pagamento</th>
								<th
									scope="col"
									style={{ cursor: "pointer" }}
									onClick={() => handleSort("dataHora")}
								>
									Data/Hora {getSortIcon("dataHora")}
								</th>
								<th scope="col">Ações</th>
							</tr>
						</thead>
						<tbody>
							{sortedGastos.map((gasto) => (
								<tr key={`${gasto.id}-${gasto.dataHora}`}>
									<td>{gasto.descricao}</td>
									<td>
										{gasto.categoria ? (
											<span
												className="badge"
												style={{
													backgroundColor: gasto.categoria.cor || getRandomColor(),
													color: "white",
												}}
											>
												{gasto.categoria.descricao}
											</span>
										) : (
											<span className="badge bg-secondary">Sem categoria</span>
										)}
									</td>
									<td>
										<strong>{formatCurrency(gasto.valor)}</strong>
									</td>
									<td>{getModalidadePagamentoText(gasto.modalidadePagamento)}</td>
									<td>{gasto.dataHora ? formatDateTime(gasto.dataHora) : "-"}</td>
									
									<td>
										<button
											className="btn btn-sm btn-outline-success me-2"
											onClick={() => onEditExpense(gasto)}
										>
											Editar
										</button>
										<button
											className="btn btn-sm btn-outline-danger"
											onClick={() => onDeleteExpense(gasto.id)}
										>
											Excluir
										</button>
									</td>
								</tr>
							))}
						</tbody>
					</table>
				</div>
			</div>
		</div>
	);
};

export default ExpenseTable;
