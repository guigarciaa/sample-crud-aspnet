
cd ../app/SampleCrud.Infra &&
dotnet ef --startup-project ../SampleCrud.API/ migrations add Init
dotnet ef --startup-project ../SampleCrud.API/ database update;

rm -rf Migrations;