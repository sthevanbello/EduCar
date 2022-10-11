-- Altera o uso do banco de dados 
USE EduCarDB;
GO

-- Insere os dados nas respectivas tabelas
INSERT INTO Cambio (Tipo) VALUES ('Manual'),('Automático');
GO

INSERT INTO Direcao (Tipo) VALUES ('Mecânica'),('Hidráulica'),('Elétrica');
GO

INSERT INTO CaracteristicasGerais (Marca, Placa, Cor, Assentos, Portas) VALUES 
('Toyota','ABC-1234','Preto', 5, 4),
('Honda','ABC-6789','Cinza', 5 , 4), 
('Hyundai','EFG-1432', 'Branco', 5, 4), 
('Fiat', 'CDF-4321', 'Vermelho', 5, 4),
('Chevrolet', 'LFB-8965', 'Azul', 5, 2);
GO

INSERT INTO FichaTecnica (Modelo, Ano, Consumo, Quilometragem, IdDirecao, IdCambio) VALUES 
('Hilux', 2022, 6, '0', 2, 2),
('Civic', 2023, 15, '0', 3, 2),
('HB20', 2023, 12, '0', 2, 2),
('Argo', 2020, 13, '30000', 2, 2),
('Celta', 2004, 15, '180000', 2, 1);
GO

INSERT INTO Endereco (Logradouro, Bairro, Cidade, Estado, Numero, CEP) VALUES 
('Rua Luiz Carlos Fraga e Silva', 'Conjunto Residencial Galo Branco', 'São José dos Campos', 'SP', '53' , '12247450'),
('Rua Canabuoca','Vila Mazzei','São Paulo','SP','85','02315030'),
('Rua da Glória','Centro','Andaraí','BA','47','46830970'),
('Rua Principal','Centro','Caimbé','BA','93','48506971'),
('Rua Hortolândia','Engenho da Rainha','Rio de Janeiro','RJ','25','20766630'),
('Travessa São João Batista','Largo do Barradas','Niterói','RJ','25','24110038'),
('Avenida Willy Conrado Bohlen','Parque Aeroporto','Taubaté','SP','110','12051381');
GO

INSERT INTO TipoUsuario (Tipo) VALUES 
('Cliente'),
('Vendedor'),
('Administrador');
GO

INSERT INTO Concessionaria (Nome, Telefone, Site, IdEndereco) VALUES 
('BRILHAUTO', '2136025896', 'www.brilhauto.com.br', 5),
('Carros', '1130485514', 'www.carros.com.br', 2),
('Belino Veículos', '7130008664', 'www.belinoveiculos.com.br', 4);
GO

INSERT INTO Usuario (Nome, Sobrenome, CPF_CNPJ, Celular, Email, Senha, Aceite, IdEndereço, IdTipoUsuario) VALUES 
('Marge','Simpson','87738061022','12977777777','maria@email.com','123456789', 1, 6, 1),
('Luigi','Mario','69197904082','71988888888','luigi@email.com','123456789', 1, 3, 1),
('Carro','System','81932668000179','11966666666','carrosystem@email.com','123456789', 1, 1, 2),
('Fernanda','Carvalho','64939033007','11999999999','fernanda@email.com','123456789', 1, 7, 3);
GO

INSERT INTO Cartao (Numero, Titular, Bandeira, CPF_CNPJ, Vencimento, CVV, IdUsuario) VALUES 
('5500611999993338','Marge Simpson','Mastercard','87738061022','03/2023','892', 1),
('4556494343206301','Luigi Mario','Visa','69197904082','07/2024','552', 2),
('344508853547112', 'Carro System', 'American Express', '81932668000179', '11/2024', '508', 3);
GO

INSERT INTO StatusVenda (Status) VALUES ('Disponível'),('Indisponível');
GO

INSERT INTO Veiculo (Nome, Valor, IdConcessionaria, IdFichaTecnica, IdCaracteristicasGerais, IdStatusVenda) VALUES 
('Toyota Hilux SRV', 268690.00, 2, 1, 1, 1),
('Honda Civic Sport 2.0', 145000.00, 2, 2, 2, 1),
('HB20 Platinum Plus', 114390.00, 1, 3, 3, 2),
('FIAT ARGO 1.0 Firefly Flex', 56490.00, 3, 4, 4, 2),
('Chevrolet Celta 1.0', 16500.00, 3, 5, 5, 2);
GO

INSERT INTO Pedido (IdUsuario, IdConcessionaria, IdVeiculo, IdCartao) VALUES 
(1, 1, 3, 1),
(2, 3, 5, 2),
(2, 3, 4, 2);
GO