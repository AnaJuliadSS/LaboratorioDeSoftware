// src/components/Gastura/GasturaApp.tsx
import React, { useState, useMemo } from 'react';
import { 
  Plus, 
  Edit, 
  Trash2, 
  Eye, 
  BarChart3, 
  User, 
  Filter
} from 'lucide-react';

import { Gasto, ModalidadePagamento, Filtros } from './types';
import { categorias, metas, gastosMock } from './data';
import { MetaCard } from './components/MetaCard';
import { GastoModal } from './components/GastoModal';

const GasturaApp: React.FC = () => {
  const [gastos, setGastos] = useState<Gasto[]>(gastosMock);
  const [selectedTab, setSelectedTab] = useState<'dashboard' | 'graficos' | 'perfil'>('dashboard');
  const [showAddModal, setShowAddModal] = useState(false);
  const [showEditModal, setShowEditModal] = useState(false);
  const [showViewModal, setShowViewModal] = useState(false);
  const [selectedGasto, setSelectedGasto] = useState<Gasto | null>(null);
  const [filtros, setFiltros] = useState<Filtros>({
    categoria: '',
    modalidade: '',
    dataInicio: '',
    dataFim: '',
    valorMin: '',
    valorMax: ''
  });

  // Funções auxiliares
  const getModalidadePagamentoText = (modalidade: ModalidadePagamento): string => {
    switch (modalidade) {
      case ModalidadePagamento.Credito: return 'Crédito';
      case ModalidadePagamento.Debito: return 'Débito';
      case ModalidadePagamento.Dinheiro: return 'Dinheiro';
      case ModalidadePagamento.Pix: return 'PIX';
      default: return '';
    }
  };

  // Cálculos
  const gastosPorCategoria = useMemo(() => {
    return gastos.reduce((acc, gasto) => {
      acc[gasto.categoriaId] = (acc[gasto.categoriaId] || 0) + gasto.valor;
      return acc;
    }, {} as Record<number, number>);
  }, [gastos]);

  const gastosFiltrados = useMemo(() => {
    return gastos.filter(gasto => {
      const matchCategoria = !filtros.categoria || gasto.categoriaId.toString() === filtros.categoria;
      const matchModalidade = !filtros.modalidade || gasto.modalidadePagamento.toString() === filtros.modalidade;
      const matchDataInicio = !filtros.dataInicio || gasto.dataHora >= new Date(filtros.dataInicio);
      const matchDataFim = !filtros.dataFim || gasto.dataHora <= new Date(filtros.dataFim);
      const matchValorMin = !filtros.valorMin || gasto.valor >= parseFloat(filtros.valorMin);
      const matchValorMax = !filtros.valorMax || gasto.valor <= parseFloat(filtros.valorMax);
      
      return matchCategoria && matchModalidade && matchDataInicio && matchDataFim && matchValorMin && matchValorMax;
    });
  }, [gastos, filtros]);

  const totalGastos = gastosFiltrados.reduce((total, gasto) => total + gasto.valor, 0);

  const handleDelete = (id: number) => {
    if (window.confirm('Tem certeza que deseja excluir este gasto?')) {
      setGastos(gastos.filter(g => g.id !== id));
    }
  };

  const handleAddGasto = (formData: any) => {
    const novoGasto: Gasto = {
      id: Date.now(),
      ...formData,
      usuarioId: 1,
      categoria: categorias.find(c => c.id === formData.categoriaId)!
    };
    setGastos([...gastos, novoGasto]);
  };

  const handleEditGasto = (formData: any) => {
    if (selectedGasto) {
      setGastos(gastos.map(g => g.id === selectedGasto.id ? {
        ...g,
        ...formData,
        categoria: categorias.find(c => c.id === formData.categoriaId)!
      } : g));
    }
  };

  return (
    <div className="min-h-screen bg-gray-50 flex">
      {/* Sidebar */}
      <div className="w-64 bg-white shadow-lg">
        <div className="p-6 border-b">
          <h1 className="text-2xl font-bold text-gray-800">Gastura</h1>
        </div>
        <nav className="mt-6">
          <button
            onClick={() => setSelectedTab('dashboard')}
            className={`w-full text-left px-6 py-3 flex items-center space-x-3 hover:bg-gray-100 ${
              selectedTab === 'dashboard' ? 'bg-blue-50 border-r-4 border-blue-500 text-blue-700' : 'text-gray-700'
            }`}
          >
            <Filter size={20} />
            <span>Dashboard</span>
          </button>
          <button
            onClick={() => setSelectedTab('graficos')}
            className={`w-full text-left px-6 py-3 flex items-center space-x-3 hover:bg-gray-100 ${
              selectedTab === 'graficos' ? 'bg-blue-50 border-r-4 border-blue-500 text-blue-700' : 'text-gray-700'
            }`}
          >
            <BarChart3 size={20} />
            <span>Visualizar gráficos</span>
          </button>
          <button
            onClick={() => setSelectedTab('perfil')}
            className={`w-full text-left px-6 py-3 flex items-center space-x-3 hover:bg-gray-100 ${
              selectedTab === 'perfil' ? 'bg-blue-50 border-r-4 border-blue-500 text-blue-700' : 'text-gray-700'
            }`}
          >
            <User size={20} />
            <span>Ver dados perfil</span>
          </button>
        </nav>
      </div>

      {/* Main Content */}
      <div className="flex-1 p-8">
        {selectedTab === 'dashboard' && (
          <div className="space-y-8">
            {/* Header */}
            <div className="flex justify-between items-center">
              <h2 className="text-3xl font-bold text-gray-800">Dashboard</h2>
              <button
                onClick={() => setShowAddModal(true)}
                className="bg-blue-600 text-white px-4 py-2 rounded-lg flex items-center space-x-2 hover:bg-blue-700 transition-colors"
              >
                <Plus size={20} />
                <span>Adicionar Gasto</span>
              </button>
            </div>

            {/* Metas */}
            <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
              {metas.map(meta => (
                <MetaCard 
                  key={meta.id} 
                  meta={meta} 
                  gastoTotal={gastosPorCategoria[meta.categoriaId] || 0}
                />
              ))}
            </div>

            {/* Filtros */}
            <div className="bg-white rounded-lg shadow-md p-6">
              <h3 className="text-lg font-semibold text-gray-800 mb-4">Filtros</h3>
              <div className="grid grid-cols-1 md:grid-cols-3 lg:grid-cols-6 gap-4">
                <select
                  value={filtros.categoria}
                  onChange={(e) => setFiltros({...filtros, categoria: e.target.value})}
                  className="px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                >
                  <option value="">Todas as categorias</option>
                  {categorias.map(cat => (
                    <option key={cat.id} value={cat.id}>{cat.nome}</option>
                  ))}
                </select>

                <select
                  value={filtros.modalidade}
                  onChange={(e) => setFiltros({...filtros, modalidade: e.target.value})}
                  className="px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                >
                  <option value="">Todas as modalidades</option>
                  <option value="1">Crédito</option>
                  <option value="2">Débito</option>
                  <option value="3">Dinheiro</option>
                  <option value="4">PIX</option>
                </select>

                <input
                  type="date"
                  value={filtros.dataInicio}
                  onChange={(e) => setFiltros({...filtros, dataInicio: e.target.value})}
                  className="px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  placeholder="Data início"
                />

                <input
                  type="date"
                  value={filtros.dataFim}
                  onChange={(e) => setFiltros({...filtros, dataFim: e.target.value})}
                  className="px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  placeholder="Data fim"
                />

                <input
                  type="number"
                  step="0.01"
                  value={filtros.valorMin}
                  onChange={(e) => setFiltros({...filtros, valorMin: e.target.value})}
                  className="px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  placeholder="Valor mínimo"
                />

                <input
                  type="number"
                  step="0.01"
                  value={filtros.valorMax}
                  onChange={(e) => setFiltros({...filtros, valorMax: e.target.value})}
                  className="px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                  placeholder="Valor máximo"
                />
              </div>
            </div>

            {/* Total */}
            <div className="bg-white rounded-lg shadow-md p-6">
              <div className="flex justify-between items-center">
                <h3 className="text-lg font-semibold text-gray-800">Total de Gastos</h3>
                <span className="text-2xl font-bold text-blue-600">
                  R$ {totalGastos.toFixed(2)}
                </span>
              </div>
            </div>

            {/* Lista de Gastos */}
            <div className="bg-white rounded-lg shadow-md overflow-hidden">
              <div className="px-6 py-4 border-b">
                <h3 className="text-lg font-semibold text-gray-800">Gastos Registrados</h3>
              </div>
              <div className="overflow-x-auto">
                <table className="w-full">
                  <thead className="bg-gray-50">
                    <tr>
                      <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Descrição</th>
                      <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Categoria</th>
                      <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Modalidade</th>
                      <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Valor</th>
                      <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Data</th>
                      <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Ações</th>
                    </tr>
                  </thead>
                  <tbody className="bg-white divide-y divide-gray-200">
                    {gastosFiltrados.map((gasto) => (
                      <tr key={gasto.id} className="hover:bg-gray-50">
                        <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{gasto.descricao}</td>
                        <td className="px-6 py-4 whitespace-nowrap">
                          <span 
                            className="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium text-white"
                            style={{ backgroundColor: gasto.categoria.cor }}
                          >
                            {gasto.categoria.nome}
                          </span>
                        </td>
                        <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                          {getModalidadePagamentoText(gasto.modalidadePagamento)}
                        </td>
                        <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">
                          R$ {gasto.valor.toFixed(2)}
                        </td>
                        <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                          {gasto.dataHora.toLocaleDateString('pt-BR')} {gasto.dataHora.toLocaleTimeString('pt-BR', { hour: '2-digit', minute: '2-digit' })}
                        </td>
                        <td className="px-6 py-4 whitespace-nowrap text-sm font-medium space-x-2">
                          <button
                            onClick={() => {
                              setSelectedGasto(gasto);
                              setShowViewModal(true);
                            }}
                            className="text-blue-600 hover:text-blue-900 inline-flex items-center"
                          >
                            <Eye size={16} />
                          </button>
                          <button
                            onClick={() => {
                              setSelectedGasto(gasto);
                              setShowEditModal(true);
                            }}
                            className="text-green-600 hover:text-green-900 inline-flex items-center ml-2"
                          >
                            <Edit size={16} />
                          </button>
                          <button
                            onClick={() => handleDelete(gasto.id)}
                            className="text-red-600 hover:text-red-900 inline-flex items-center ml-2"
                          >
                            <Trash2 size={16} />
                          </button>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        )}

        {selectedTab === 'graficos' && (
          <div className="text-center py-20">
            <BarChart3 size={64} className="mx-auto text-gray-400 mb-4" />
            <h2 className="text-2xl font-bold text-gray-800 mb-2">Gráficos</h2>
            <p className="text-gray-600">Visualização de gráficos em desenvolvimento</p>
          </div>
        )}

{selectedTab === 'perfil' && (
          <div className="text-center py-20">
            <User size={64} className="mx-auto text-gray-400 mb-4" />
            <h2 className="text-2xl font-bold text-gray-800 mb-2">Perfil do Usuário</h2>
            <p className="text-gray-600">Dados do perfil em desenvolvimento</p>
          </div>
        )}
      </div>

      {/* Modals */}
      <GastoModal 
              isOpen={showAddModal}
              onClose={() => setShowAddModal(false)}
              mode="add" categorias={[]} onSubmit={function (formData: any): void {
                  throw new Error('Function not implemented.');
              } }      />
      
      <GastoModal 
              isOpen={showEditModal}
              onClose={() => setShowEditModal(false)}
              gasto={selectedGasto}
              mode="edit" categorias={[]} onSubmit={function (formData: any): void {
                  throw new Error('Function not implemented.');
              } }      />
      
      <GastoModal 
              isOpen={showViewModal}
              onClose={() => setShowViewModal(false)}
              gasto={selectedGasto}
              mode="view" categorias={[]} onSubmit={function (formData: any): void {
                  throw new Error('Function not implemented.');
              } }      />
    </div>
  );
};

export default GasturaApp;