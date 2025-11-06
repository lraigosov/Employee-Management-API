up:
	docker compose up -d --build

down:
	docker compose down -v

test:
	dotnet test --logger:"trx;LogFileName=test.trx"

migrate:
	dotnet ef database update --project src/EmployeeManagement.Infrastructure --startup-project src/EmployeeManagement.Api

build:
	dotnet build --configuration Release

clean:
	dotnet clean

restore:
	dotnet restore

run:
	dotnet run --project src/EmployeeManagement.Api
