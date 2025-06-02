CREATE TABLE Produtos (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(100) NOT NULL,
    Descricao NVARCHAR(255),
    Preco DECIMAL(10,2) NOT NULL,
    Quantidade INT NOT NULL,
    FornecedorID INT,
    FOREIGN KEY (FornecedorID) REFERENCES Fornecedores(ID)
);