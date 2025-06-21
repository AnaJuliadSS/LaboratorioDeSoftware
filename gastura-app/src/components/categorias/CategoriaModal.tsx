// CategoriaModal.tsx
import React, { useState, useEffect } from 'react';

interface CategoriaModalProps {
  show: boolean;
  onHide: () => void;
  onSave: (categoria: { descricao: string; cor: string }) => void;
}

const CategoriaModal: React.FC<CategoriaModalProps> = ({ show, onHide, onSave }) => {
  const [descricao, setDescricao] = useState('');
  const [cor, setCor] = useState('#000000');

  useEffect(() => {
    if (show) {
      setDescricao('');
      setCor('#000000');
    }
  }, [show]);

  const handleSubmit = () => {
    if (!descricao.trim()) {
      alert('A descrição é obrigatória');
      return;
    }
    onSave({ descricao, cor });
    onHide();
  };

  if (!show) return null;

  return (
    <div className="modal show d-block" style={{ backgroundColor: "rgba(0,0,0,0.5)" }}>
      <div className="modal-dialog">
        <div className="modal-content">
          <div className="modal-header">
            <h5 className="modal-title">Nova Categoria</h5>
            <button className="btn-close" onClick={onHide}></button>
          </div>
          <div className="modal-body">
            <div className="mb-3">
              <label className="form-label">Descrição</label>
              <input
                type="text"
                className="form-control"
                value={descricao}
                onChange={(e) => setDescricao(e.target.value)}
              />
            </div>
            <div className="mb-3">
              <label className="form-label">Cor</label>
              <input
                type="color"
                className="form-control form-control-color"
                value={cor}
                onChange={(e) => setCor(e.target.value)}
              />
            </div>
          </div>
          <div className="modal-footer">
            <button className="btn btn-secondary" onClick={onHide}>Cancelar</button>
            <button className="btn btn-primary" onClick={handleSubmit}>Salvar</button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default CategoriaModal;