import { Menu } from "lucide-react";

interface HeaderProps {
  onMenuClick: () => void;
  title: string;
}

// Componente Header (para mobile)
const Header: React.FC<HeaderProps> = ({ onMenuClick, title }) => {  return (
    <header className="lg:hidden bg-white shadow-sm border-b border-gray-200 px-4 py-3 flex items-center justify-between">
      <button
        onClick={onMenuClick}
        className="p-2 rounded-md hover:bg-gray-100 transition-colors"
      >
        <Menu size={20} className="text-gray-600" />
      </button>
      <h1 className="text-lg font-semibold text-gray-800">{title}</h1>
      <div className="w-8" /> {/* Spacer para centralizar o título */}
    </header>
  );
};

export default Header;