## MeetingRoomBackEnd - API RESTFULL para gerenciamento das salas de reuniões

**1-** Baixar o SDK do .NETCore 2.2 [aqui](https://dotnet.microsoft.com/download/thank-you/dotnet-sdk-2.2.402-windows-x64-installer).

**2-** Restaurar o banco de dados que está localizado no diretório "Utils" da aplicação com o nome "MeetingRoom.sql";

### Executando a aplicação:

Para executar a a API corretamente é necessário atentar-se a alguns detalhes:

**1-** O banco de dados utilizado na aplicação foi o SQL Server Express 2017.

**2-** Para que a API comunique com o banco de dados sem problemas é necessário estar atento à string de conexão, que fica localizada no arquivo "appsettings.json"
     
      A string de conxão correta para a aplicação é: 
      
      "MeetingRoom": "Server=[Nome do Servidor];Database=MeetingRoom;User ID=sa;Password=[Senha do usuário sa];Trusted_Connection=true;"

**3-** Depois bastar executar a aplicação pelo visual studio ou pela linha de comando, e abrir o navegador na url informada.

**4-** Se aparecer a documentação da API feita pelo Sweagger está tudo correto.

**Qualquer problema basta entrar em contato pelo email willian_menezes_santos@hotmail.com**
