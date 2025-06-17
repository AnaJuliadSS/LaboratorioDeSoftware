// Componente GastoModal
  const GastoModal: React.FC<{ 
    isOpen: boolean; 
    onClose: () => void; 
    gasto?: Gasto | null;
    mode: 'add' | 'edit' | 'view'
  }> = ({ isOpen, onClose, gasto, mode }) => {
    const [formData, setFormData] = useState({
      valor: gasto?.valor || 0,
      descricao: gasto?.descricao || '',
      categoriaId: gasto?.categoriaId || 1,
      modalidadePagamento: gasto?.modalidadePagamento || ModalidadePagamento.Credito,
      dataHora: gasto?.dataHora || new Date()
    });

    React.useEffect(() => {
      if (gasto && isOpen) {
        setFormData({
          valor: gasto.valor,
          descricao: gasto.descricao,
          categoriaId: gasto.categoriaId,
          modalidadePagamento: gasto.modalidadePagamento,
          dataHora: gasto.dataHora
        });
      }
    }, [gasto, isOpen]);

    if (!isOpen) return null;

    const handleSubmit = () => {
      if (mode === 'add') {
        const novoGasto: Gasto = {
          id: Date.now(),
          ...formData,
          usuarioId: 1,
          categoria: categorias.find(c => c.id === formData.categoriaId)!
        };
        setGastos([...gastos, novoGasto]);
      } else if (mode === 'edit' && gasto) {
        setGastos(gastos.map(g => g.id === gasto.id ? {
          ...g,
          ...formData,
          categoria: categorias.find(c => c.id === formData.categoriaId)!
        } : g));
      }
      onClose();
    };

    return (
      <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
        <div className="bg-white rounded-lg p-6 w-full max-w-md mx-4">
          <h2 className="text-xl font-bold mb-4">
            {mode === 'add' ? 'Adicionar Gasto' : mode === 'edit' ? 'Editar Gasto' : 'Visualizar Gasto'}
          </h2>
          
          <div className="space-y-4">
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">Descrição</label>
              <input
                type="text"
                value={formData.descricao}
                onChange={(e) => setFormData({...formData, descricao: e.target.value})}
                className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                disabled={mode === 'view'}
              />
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">Valor</label>
              <input
                type="number"
                step="0.01"
                value={formData.valor}
                onChange={(e) => setFormData({...formData, valor: parseFloat(e.target.value) || 0})}
                className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                disabled={mode === 'view'}
              />
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">Categoria</label>
              <select
                value={formData.categoriaId}
                onChange={(e) => setFormData({...formData, categoriaId: parseInt(e.target.value)})}
                className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                disabled={mode === 'view'}
              >
                {categorias.map(cat => (
                  <option key={cat.id} value={cat.id}>{cat.nome}</option>
                ))}
              </select>
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">Modalidade de Pagamento</label>
              <select
                value={formData.modalidadePagamento}
                onChange={(e) => setFormData({...formData, modalidadePagamento: parseInt(e.target.value) as ModalidadePagamento})}
                className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                disabled={mode === 'view'}
              >
                <option value={ModalidadePagamento.Credito}>Crédito</option>
                <option value={ModalidadePagamento.Debito}>Débito</option>
                <option value={ModalidadePagamento.Dinheiro}>Dinheiro</option>
                <option value={ModalidadePagamento.Pix}>PIX</option>
              </select>
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">Data e Hora</label>
              <input
                type="datetime-local"
                value={formData.dataHora.toISOString().slice(0, 16)}
                onChange={(e) => setFormData({...formData, dataHora: new Date(e.target.value)})}
                className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                disabled={mode === 'view'}
              />
            </div>

            <div className="flex justify-end space-x-3 pt-4">
              <button
                type="button"
                onClick={onClose}
                className="px-4 py-2 text-gray-600 border border-gray-300 rounded-md hover:bg-gray-50"
              >
                {mode === 'view' ? 'Fechar' : 'Cancelar'}
              </button>
              {mode !== 'view' && (
                <button
                  type="button"
                  onClick={handleSubmit}
                  className="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700"
                >
                  {mode === 'add' ? 'Adicionar' : 'Salvar'}
                </button>
              )}
            </div>
          </div>
        </div>
      </div>
    );
  };