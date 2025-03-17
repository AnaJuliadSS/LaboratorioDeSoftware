# Requisitos Funcionais

1. RNF01 - Registro de Gastos <br>
O usuário pode registrar cada gasto com os seguintes dados:
    - Descrição do gasto (ex: “Almoço no restaurante”)
    - Valor (ex: R$ 35,00)
    - Categoria (Alimentação, Transporte, Lazer, etc.)
    - Data e horário do gasto

2. RNF02 - Visualização de Gastos <br>
O usuário deverá conseguir visualizar os gastos registrados.

    - O sistema exibirá todos os gastos registrados em uma lista ordenada pelo atributo desejado.
    - O usuário pode filtrar a lista por categoria.

3. RNF03 - Cálculo de Total de Gastos <br>
O sistema deverá calcular o total de gastos do usuário.

    - O software calculará automaticamente o total de gastos no mês, apresentando ao usuário a quantia total gasta e comparando com o valor previsto de orçamento (se o usuário definir).


4. RNF04 - Controle de Orçamento Pessoal <br>
O usuário pode definir um orçamento total permitido por categoria ou por mês.

    - O sistema compara o que foi gasto com o limite estabelecido, oferecendo alertas em caso de aproximação do limite.

    - O software enviará alertas quando o usuário ultrapassar o limite de gastos definidos em uma categoria ou no total mensal.

5. RNF05 - Alertas e Notificações <br>
O sistema deverá alertar o usuário quando o mesmo estiver próximo ou ultrapassado o limite de gastos estabelecido, tanto por mês quanto por categoria.


6. RNF06 - Relatórios e Estatísticas <br>
O sistema deve exibir de forma visual e gráfica a distribuição de gastos do usuário.

    - O sistema irá gerar gráficos simples para mostrar a distribuição dos gastos por categoria no mês.
    - Relatórios sumarizados para análise de tendências financeiras do usuário (gastos por categoria, mês, etc.).

# Requisitos Não Funcionais

1. RNF01 - Interface Intuitiva <br>
O sistema deve possuir uma interface intuitiva.

2. RNF02 - Desempenho <br>
O tempo de resposta para o carregamento da lista de gastos não deve ultrapassar **2 segundos**.

3. RNF03 - Segurança <br>
Deve haver proteção contra ataques comuns, como **SQL Injection, XSS e CSRF**.

4. RNF04 - Armazenamento de Dados <br>
Os registros de gastos devem ser armazenados em um banco de dados **seguro e confiável**.

5. RNF05 - Compatibilidade <br>
O sistema deve ser compatível com os principais navegadores modernos (**Chrome, Firefox, Edge, Safari**).

6. RNF06 - Manutenibilidade <br>
O código deve ser **modular**, permitindo futuras atualizações e melhorias.
