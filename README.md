# Case Equals

## Pre-Requisitos
1. - Rodar migrations do EF:
   "dotnet ef --startup-project ../Equals.Case.WebAPI/  migrations add init"
2. - Rodar update database do EF:
   "dotnet ef --startup-project ../Equals.Case.WebAPI  database update"

## Pre-cadastros para testes
1. - Cadatrar Periodicidades (Diária e Semanal)
2. - Cadastrar Adquirentes (UFLACARD e FagammonCard)
3. - Cadastrar previsao de recebimento 
4. - Enviar chamada post com arquivo (ou string diretamente atribuída ao nome, ou arquivo em base64 , campo 'fileBytes')

