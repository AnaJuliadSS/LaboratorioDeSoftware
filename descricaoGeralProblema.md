# Sistema de Controle de Gastos Pessoais

### Gastura – Gerenciador de Gastos Pessoais

Atualmente, muitas pessoas enfrentam dificuldades para controlar seus gastos diários e manter um planejamento financeiro eficiente. A falta de um sistema organizado pode levar a despesas excessivas e falta de controle sobre o orçamento pessoal. Além disso, a falta de controle sobre as despesas pode gerar ansiedade e frustração, uma vez que essa situação representa um fator de incerteza. A ausência de planejamento financeiro impede a realização de planos baseados na renda, tornando-a instável e dificultando a organização financeira do indivíduo.

O Gastura tem como objetivo auxiliar os usuários a registrar seus gastos de forma organizada, visualizar seus hábitos de consumo e estabelecer limites financeiros. O sistema permitirá registrar despesas, calcular o total gasto no mês, definir orçamentos e receber alertas quando os limites forem atingidos, promovendo maior controle financeiro.

## Descrição dos casos de Uso

### C01 - Registrar gasto

* **Descrição**: Permite que o usuário cadastre um novo gasto informando descrição, valor, categoria e data/hora.

* **Ator**: Usuário.

* **Pré-condição**: O sistema deve estar funcional.

* **Fluxo Principal**: <br>  
    - O usuário acessa a funcionalidade de registro de gastos.
    - O usuário preenche as informações do gasto.
    - O sistema armazena e exibe na lista de gastos.

### C02 - Visualizar gastos

* **Descrição**: Permite que o usuário veja a lista de gastos registrados e os organize conforme critérios escolhidos.

* **Ator**: Usuário.

* **Pré-condição**: O usuário deve ter pelo menos um gasto registrado.

* **Fluxo Principal**:  
    - O usuário acessa a tela de visualização de gastos.  
    - O sistema exibe todos os gastos registrados.  
    - O usuário pode ordenar e filtrar os gastos por categoria.  

### C03 - Calcular total de gastos

* **Descrição**: O sistema calcula automaticamente o total de gastos no mês e exibe a comparação com o orçamento previsto.

* **Ator**: Sistema.

* **Pré-condição**: O usuário deve ter registrado gastos e o orçamento previsto.

* **Fluxo Principal**:  
    - O sistema soma os gastos do mês.  
    - O sistema compara o total com o orçamento definido.  
    - Exibe ao usuário o total gasto e a diferença em relação ao orçamento.  

### C04 - Definir orçamento

* **Descrição**: Permite que o usuário defina um orçamento mensal total ou por categoria.

* **Ator**: Usuário.

* **Pré-condição**: O sistema deve estar funcional.

* **Fluxo Principal**:  
    - O usuário acessa a funcionalidade de definição de orçamento.  
    - O usuáro define um limite de gastos total ou por categoria.  
    - O sistema armazena a configuração.  

### C05 - Emitir alertas de gastos

* **Descrição**: O sistema exibe um alerta visual para o usuário quando ele estiver próximo ou ultrapassar o orçamento definido.

* **Ator**: Sistema.

* **Pré-condição**: O usuário deve ter um orçamento definido e gastos registrados.

* **Fluxo Principal**:  
    - O sistema monitora os gastos do usuário.  
    - Se o limite for atingido, o sistema emite um alerta visual.  

### C06 - Gerar relatórios

* **Descrição**: O sistema exibe gráficos e relatórios com a distribuição dos gastos do usuário.

* **Ator**: Usuário.

* **Pré-condição**: O usuário deve ter registrado gastos.

* **Fluxo Principal**:  
    - O usuário solicita um relatório.  
    - O sistema gera gráficos sobre a distribuição dos gastos por categoria e mês.  
    - O sistema exibe os dados de forma visual e sumarizada.  