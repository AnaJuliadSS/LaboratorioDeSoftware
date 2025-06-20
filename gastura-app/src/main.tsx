import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import App from "./App.tsx";
import "../src/index.css";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import PrivateRoute from "./PrivateRoute.tsx";
import { ChakraProvider, defaultSystem } from "@chakra-ui/react";
import Login from "./components/login/Login.tsx";
import Cadastro from "./components/cadastro/Cadastro.tsx";
import GasturaApp from "./components/home/GasturaApp.tsx";

createRoot(document.getElementById("root")!).render(
	<StrictMode>
		<ChakraProvider value={defaultSystem}>
			<BrowserRouter>
				<Routes>
					<Route path="/" element={<Login />} />
					<Route path="/register" element={<Cadastro />} />
					{/* <Route element={<PrivateRoute />}>
						<Route path="/home" element={<App />} />
					</Route> */}
					<Route path="/home" element={<GasturaApp />} />
				</Routes>
			</BrowserRouter>
		</ChakraProvider>
	</StrictMode>
);
