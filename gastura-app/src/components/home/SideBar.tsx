import { BarChart3, Home, User, X } from "lucide-react";
import MenuItem from "../../types/MenuItems";

interface SidebarProps {
  isOpen: boolean;
  onClose: () => void;
  activeItem: string;
  onItemClick: (id: string) => void;
  isMobile: boolean;
}

const Sidebar: React.FC<SidebarProps> = ({ isOpen, onClose, activeItem, onItemClick, isMobile }) => {
    
const menuItems: MenuItem[] = [
    { id: 'dashboard', label: 'Dashboard', icon: Home },
    { id: 'graficos', label: 'Visualizar gráficos', icon: BarChart3 },
    { id: 'perfil', label: 'Perfil', icon: User }
  ];

  const sidebarClasses = `
    fixed top-0 left-0 h-full bg-white shadow-lg transition-transform duration-300 ease-in-out z-50
    ${isMobile ? 'w-72' : 'w-64'}
    ${isOpen ? 'translate-x-0' : '-translate-x-full'}
    ${!isMobile ? 'lg:translate-x-0' : ''}
  `;

  return (
    <>
      {/* Overlay para mobile */}
      {isMobile && isOpen && (
        <div 
          className="fixed inset-0 bg-black bg-opacity-50 z-40 lg:hidden"
          onClick={onClose}
        />
      )}
      
      {/* Sidebar */}
      <div className={sidebarClasses}>
        <div className="flex flex-col h-full">
          {/* Header do Sidebar */}
          <div className="flex items-center justify-between p-4 border-b border-gray-200">
            <h2 className="text-lg font-semibold text-gray-800">Menu</h2>
            {isMobile && (
              <button
                onClick={onClose}
                className="p-1 rounded-md hover:bg-gray-100 transition-colors"
              >
                <X size={20} className="text-gray-600" />
              </button>
            )}
          </div>
          
          {/* Menu Items */}
          <nav className="flex-1 p-4">
            <ul className="space-y-2">
              {menuItems.map((item) => {
                const IconComponent = item.icon;
                const isActive = activeItem === item.id;
                
                return (
                  <li key={item.id}>
                    <button
                      onClick={() => onItemClick(item.id)}
                      className={`
                        w-full flex items-center px-4 py-3 rounded-lg text-left transition-colors duration-200
                        ${isActive 
                          ? 'bg-blue-50 text-blue-700 border-r-2 border-blue-700' 
                          : 'text-gray-700 hover:bg-gray-50'
                        }
                      `}
                    >
                      <IconComponent 
                        size={20} 
                        className={`mr-3 ${isActive ? 'text-blue-700' : 'text-gray-500'}`} 
                      />
                      <span className="font-medium">{item.label}</span>
                    </button>
                  </li>
                );
              })}
            </ul>
          </nav>
          
          {/* Footer do Sidebar */}
          <div className="p-4 border-t border-gray-200">
            <div className="text-sm text-gray-500 text-center">
              Controle de Gastos v1.0
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default Sidebar;