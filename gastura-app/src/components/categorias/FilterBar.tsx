import React, { useState } from 'react';
import Categoria from '../../types/Categoria';

const FilterBar: React.FC<{
  categorias: Categoria[],
  onFilterChange: (filters: any) => void,
  onAddExpense: () => void
}> = ({ categorias, onFilterChange, onAddExpense }) => {
  const [filters, setFilters] = useState({
    categoria: '',
    modalidadePagamento: '',
    dataInicio: '',
    dataFim: '',
    descricao: ''
  });

  const handleFilterChange = (key: string, value: string) => {
    const newFilters = { ...filters, [key]: value };
    setFilters(newFilters);
    onFilterChange(newFilters);
  };

  return (
    <div className="card mb-4">
      <div className="card-body">
        <div className="row align-items-end">
          <div className="col-md-2 mb-2">
            <button className="btn btn-warning w-100" onClick={onAddExpense}>
              Adicionar
            </button>
          </div>
          
          <div className="col-md-2 mb-2">
            <label className="form-label">Categoria</label>
            <select 
              className="form-select"
              value={filters.categoria}
              onChange={(e) => handleFilterChange('categoria', e.target.value)}
            >
              <option value="">Todas</option>
              {categorias.map(cat => (
                <option key={cat.id} value={cat.id.toString()}>{cat.descricao}</option>
              ))}
            </select>
          </div>

          <div className="col-md-2 mb-2">
            <label className="form-label">Pagamento</label>
            <select 
              className="form-select"
              value={filters.modalidadePagamento}
              onChange={(e) => handleFilterChange('modalidadePagamento', e.target.value)}
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
              onChange={(e) => handleFilterChange('dataInicio', e.target.value)}
            />
          </div>

          <div className="col-md-2 mb-2">
            <label className="form-label">Data Fim</label>
            <input 
              type="date" 
              className="form-control"
              value={filters.dataFim}
              onChange={(e) => handleFilterChange('dataFim', e.target.value)}
            />
          </div>

          <div className="col-md-2 mb-2">
            <label className="form-label">Descrição</label>
            <input 
              type="text" 
              className="form-control"
              placeholder="Buscar..."
              value={filters.descricao}
              onChange={(e) => handleFilterChange('descricao', e.target.value)}
            />
          </div>
        </div>
      </div>
    </div>
  );
};

export default FilterBar;