import GasturaApp from "./GasturaApp";
import Header from "./Header";
import React, { ReactNode, useState } from "react";
import Sidebar from "./SideBar";

// Interface para as props
interface LayoutProps {
  children?: ReactNode; // opcional, caso queira renderizar children diretamente
}

// Componente de Layout Principal
const Layout: React.FC<LayoutProps> = () => {
  const [sidebarOpen, setSidebarOpen] = useState(false);
  const [activeItem, setActiveItem] = useState('dashboard');
  const [isMobile, setIsMobile] = useState(window.innerWidth < 1024);

  // Detectar mudanças no tamanho da tela
  React.useEffect(() => {
    const handleResize = () => {
      const mobile = window.innerWidth < 1024;
      setIsMobile(mobile);
      if (!mobile) {
        setSidebarOpen(false); // Fechar sidebar no desktop
      }
    };

    window.addEventListener('resize', handleResize);
    return () => window.removeEventListener('resize', handleResize);
  }, []);

  const handleMenuClick = () => {
    setSidebarOpen(!sidebarOpen);
  };

  const handleSidebarClose = () => {
    setSidebarOpen(false);
  };

  const handleItemClick = (itemId: string) => {
    setActiveItem(itemId);
    if (isMobile) {
      setSidebarOpen(false);
    }
  };

  const getPageTitle = () => {
    switch (activeItem) {
      case 'dashboard': return 'Dashboard';
      case 'graficos': return 'Gráficos';
      case 'perfil': return 'Perfil';
      default: return 'Dashboard';
    }
  };

  return (
    <div className="min-h-screen bg-gray-50">
      {/* Header Mobile */}
      <Header onMenuClick={handleMenuClick} title={getPageTitle()} />
      
      {/* Sidebar */}
      <Sidebar
        isOpen={sidebarOpen}
        onClose={handleSidebarClose}
        activeItem={activeItem}
        onItemClick={handleItemClick}
        isMobile={isMobile}
      />
      
      {/* Conteúdo Principal */}
      <main className={`transition-all duration-300 ease-in-out ${!isMobile ? 'lg:ml-64' : ''}`}>
        <div className="p-4 lg:p-6">
          {/* Renderizar conteúdo baseado no item ativo */}
          {activeItem === 'dashboard' && (
            <GasturaApp />
          )}
          {activeItem === 'graficos' && (
            <div className="bg-white rounded-lg shadow p-6">
              <h2 className="text-2xl font-bold mb-4">Visualizar Gráficos</h2>
              <p className="text-gray-600">Conteúdo dos gráficos será implementado aqui.</p>
            </div>
          )}
          {activeItem === 'perfil' && (
            <div className="bg-white rounded-lg shadow p-6">
              <h2 className="text-2xl font-bold mb-4">Perfil do Usuário</h2>
              <p className="text-gray-600">Dados do perfil serão implementados aqui.</p>
            </div>
          )}
        </div>
      </main>
    </div>
  );
};

export default Layout;