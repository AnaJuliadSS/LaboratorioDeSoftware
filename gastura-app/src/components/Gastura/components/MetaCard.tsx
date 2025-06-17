// src/components/Gastura/components/MetaCard.tsx
import React from 'react';
import { AlertTriangle, CheckCircle } from 'lucide-react';
import { Meta } from '../types';

interface MetaCardProps {
  meta: Meta;
  gastoTotal: number;
}

export const MetaCard: React.FC<MetaCardProps> = ({ meta, gastoTotal }) => {
  const percentual = (gastoTotal / meta.valor) * 100;
  
  const getStatusMeta = (gastoTotal: number, metaValor: number) => {
    const percentual = (gastoTotal / metaValor) * 100;
    if (percentual >= 100) return { status: 'danger', color: 'bg-red-500', textColor: 'text-red-700' };
    if (percentual >= 70) return { status: 'warning', color: 'bg-yellow-500', textColor: 'text-yellow-700' };
    return { status: 'success', color: 'bg-green-500', textColor: 'text-green-700' };
  };

  const { status, color, textColor } = getStatusMeta(gastoTotal, meta.valor);
  
  return (
    <div className={`bg-white rounded-lg shadow-md p-6 border-l-4 ${
      status === 'danger' ? 'border-red-500' : 
      status === 'warning' ? 'border-yellow-500' : 'border-green-500'
    }`}>
      <div className="flex items-center justify-between mb-4">
        <h3 className="text-lg font-semibold text-gray-800">{meta.categoria.nome}</h3>
        {status === 'danger' && <AlertTriangle className="text-red-500" size={20} />}
        {status === 'warning' && <AlertTriangle className="text-yellow-500" size={20} />}
        {status === 'success' && <CheckCircle className="text-green-500" size={20} />}
      </div>
      <div className="space-y-2">
        <div className="flex justify-between text-sm">
          <span className="text-gray-600">Gasto</span>
          <span className={`font-medium ${textColor}`}>
            R$ {gastoTotal.toFixed(2)}
          </span>
        </div>
        <div className="flex justify-between text-sm">
          <span className="text-gray-600">Meta</span>
          <span className="font-medium text-gray-800">R$ {meta.valor.toFixed(2)}</span>
        </div>
        <div className="w-full bg-gray-200 rounded-full h-2">
          <div 
            className={`h-2 rounded-full transition-all duration-300 ${color}`}
            style={{ width: `${Math.min(percentual, 100)}%` }}
          />
        </div>
        <div className="text-right text-xs text-gray-500">
          {percentual.toFixed(1)}% da meta
        </div>
      </div>
    </div>
  );
};