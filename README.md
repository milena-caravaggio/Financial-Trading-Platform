## Financial-Trading-Platform
`FinancialTradingService` é um serviço de processamento de dados em tempo real desenvolvido em .NET 8.0, destinado a plataformas de trading financeiro. Este serviço gerencia a recepção, processamento e transmissão de dados de mercado, oferecendo cálculos de indicadores técnicos como SMA e MACD, essenciais para decisões de trading baseadas em dados.


## Features
- **Recepção de Dados em Tempo Real**: Implementação de um servidor TCP robusto que lida com altas cargas de dados de mercado em formato JSON.
- **Cálculo de Indicadores Financeiros**: Algoritmos otimizados para calcular indicadores como SMA (Simple Moving Average) e MACD (Moving Average Convergence Divergence) com precisão e eficiência.
- **Calculation** Services: Serviços para cálculo de SMA e MACD.

## Technologies
- **.NET 8.0**: Aproveitando as mais recentes melhorias de performance e funcionalidades do framework.
- **C# 10**: Utilização de funcionalidades modernas do C# para escrita de código mais limpo e performático.
- **Docker**: Para containerização e gerenciamento simplificado de infraestrutura.
- **GitHub Actions**: Para automação de CI/CD.

## Components
- **TCP Server**: Gerencia conexões e dados de entrada.
- **JMeter**: É utilizado para criar testes de carga e simular o comportamento de múltiplos usuários.

## Architecture
- Este projeto utiliza uma arquitetura baseada em serviços, onde cada componente é responsável por uma tarefa específica (recepção de dados, processamento de indicadores). Que garante a escalabilidade e a manutenção simplificada.

![image](https://github.com/milena-caravaggio/Financial-Trading-Platform/assets/48251038/3f9e9027-4595-44ec-909c-9f89d1263137)

![image](https://github.com/milena-caravaggio/Financial-Trading-Platform/assets/48251038/50ee7a86-51ee-491c-a311-3196fff8440c)

![image](https://github.com/milena-caravaggio/Financial-Trading-Platform/assets/48251038/9f22376f-ad05-42a7-b562-584098e56463)

![image](https://github.com/milena-caravaggio/Financial-Trading-Platform/assets/48251038/73808e46-5948-43bc-80a0-ff43775d4839)

## Simulador

Quando a aplicação sobe faz uma simulação dos calculos MACD e SMA onde é exposto os resultados

## Testes com Docker


![image](https://github.com/milena-caravaggio/Financial-Trading-Platform/assets/48251038/38881e92-c6bb-4afc-a771-ecd8dddb95aa)

![image](https://github.com/milena-caravaggio/Financial-Trading-Platform/assets/48251038/b93bc7f1-20d7-44b1-ae9b-8adf3c058bc7)

![image](https://github.com/milena-caravaggio/Financial-Trading-Platform/assets/48251038/c2a5a386-5389-428d-8286-d642aad476ed)

![image](https://github.com/milena-caravaggio/Financial-Trading-Platform/assets/48251038/706bde08-e679-45e8-ba66-022cd03cd7e0)



## Testes com JMeter
- Abra o JMeter e crie um plano de testes adequado ao cenário que você deseja simular.
- Configure os samplers TCP para apontar para o endereço IP e porta onde o Worker está escutando.
- Defina o número de threads (usuários) e o período de ramp-up conforme necessário.
- Execute o plano de testes e observe os resultados para garantir que o Worker está processando os dados corretamente e performando como esperado.


![image](https://github.com/milena-caravaggio/Financial-Trading-Platform/assets/48251038/171161b2-b0ac-453f-b5f1-39abf69a3b1f)

![image](https://github.com/milena-caravaggio/Financial-Trading-Platform/assets/48251038/432ef478-d250-4719-8398-36745a0ac86b)

![image](https://github.com/milena-caravaggio/Financial-Trading-Platform/assets/48251038/bddf7685-7003-47fb-b644-29c7b28fe412)


Resultados SMAResults organizados

![image](https://github.com/milena-caravaggio/Financial-Trading-Platform/assets/48251038/b79602d4-ed87-4cb0-8035-0ad99a16fdbe)

Resultados MACDResults organizado

![image](https://github.com/milena-caravaggio/Financial-Trading-Platform/assets/48251038/23d2d925-e16a-4c70-b1d2-e1b453dba34c)


## **Autor**: Milena Lima Caravaggio Acquesta - Software Developer

