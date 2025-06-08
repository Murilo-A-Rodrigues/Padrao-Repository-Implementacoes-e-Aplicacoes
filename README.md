# Problemas de Programação Orientada a Objetos (Associações e Herança)

Este é um projeto desenvolvido na matéria de Programação Orientada a Objetos. Ele utiliza a linguagem de programação C# para resolver vários problemas abordando diferentes contextos e explora conceitos como associações, herança, composição, polimorfismo, repositórios genéricos, serviços e validação de regras de negócio.

## Projetos e Funcionalidades

### 1. **Gerenciador de Catálogo de Produtos**
- Cadastro de produtos com nome, descrição, preço e estoque.
- Listagem e remoção de produtos.
- Persistência dos dados em arquivos JSON usando repositório genérico.

### 2. **Biblioteca de Músicas Pessoais com Repositório Genérico**
- Cadastro de músicas, artistas, álbuns e duração.
- Busca de músicas por ID.
- Listagem e remoção de músicas.
- Uso de repositório genérico para persistência.

### 3. **Catálogo de Filmes com Consultas por Gênero**
- Cadastro de filmes com título, diretor, ano e gênero (enum).
- Consulta de filmes por gênero.
- Listagem e exclusão de filmes.
- Persistência em arquivos JSON.

### 4. **Sistema de Gerenciamento de Funcionários e Departamentos**
- Cadastro de departamentos e funcionários.
- Associação entre funcionários e departamentos.
- Listagem e exclusão de funcionários.
- Validação de dados e uso de repositório genérico.

### 5. **Registro de Pacientes em Clínica com Filtro por Idade**
- Cadastro de pacientes com nome, idade e informações clínicas.
- Filtro de pacientes por faixa etária.
- Listagem e remoção de pacientes.
- Persistência dos dados em arquivos.

### 6. **Inventário de Equipamentos de TI**
- Cadastro de equipamentos, tipos, número de série e data de aquisição.
- Listagem e remoção de equipamentos.
- Validação de dados e persistência em arquivos JSON.

### 7. **Sistema de Pedidos de Restaurante**
- Cadastro de itens de cardápio, pedidos e itens do pedido.
- Associação entre pedidos e itens do cardápio.
- Cálculo automático do valor total do pedido.
- Menu interativo para registrar pedidos e gerenciar o cardápio.

### 8. **Streaming**
- Cadastro de mídias: filmes, séries (com episódios) e documentários.
- Herança: Midia como superclasse, subclasses com atributos específicos.
- Composição: Série contém episódios.
- Exibição polimórfica de resumos.
  
### 9. **Sistema de Reservas de Hotel com Status**
- Cadastro de reservas com hóspede, datas e status (enum).
- Consulta de reservas por status.
- Validação de datas e exclusão de reservas.
- Repositório especializado para reservas.

### 10. **Plataforma de Cursos Online**
- Cadastro de cursos online, com nome, instrutor e carga horária.
- Validação para impedir cursos duplicados.
- Listagem e exclusão de cursos.
- Camada de serviço com regras de negócio e uso de repositório genérico.

## Como Executar

1. Certifique-se de ter o .NET SDK instalado.
2. Abra o terminal do Program do projeto desejado.
3. Execute:
```bash
   dotnet run
```

## Estrutura dos Projetos

- **Model**: Classes de domínio (entidades, regras de negócio).
- **Persistencia**: Repositórios genéricos e específicos, manipulação de arquivos.
- **Servico**: Camada de serviço com regras de negócio (quando aplicável).
- **Console**: Ponto de entrada do programa, menus e interação com o usuário.

## Estratégias e Conceitos Utilizados

- **Herança e Polimorfismo**: Superclasses abstratas e métodos sobrescritos para comportamentos específicos.
- **Composição**: Objetos que só existem dentro de outros (ex: itens em pedidos, arquivos em pastas).
- **Associação Muitos-para-Muitos**: Classes intermediárias para relacionar entidades (ex: pedidos, matrículas).
- **Encapsulamento**: Propriedades privadas e métodos públicos para manipulação segura dos dados.
- **Validação de Regras de Negócio**: Restrições implementadas nas classes de domínio (ex: duplicidade, datas únicas).
- **Repositórios Genéricos**: Centralização da lógica de persistência e redução de duplicidade de código.

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).


## Créditos

Este projeto foi desenvolvido por Murilo Andre Rodrigues como parte da disciplina de Programação Orientada a Objetos.

## Contato

- **Email**: murilorodrigues@alunos.utfpr.edu.br
- **GitHub**: Murilo-A-Rodrigues(https://github.com/Murilo-A-Rodrigues)