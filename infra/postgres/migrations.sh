
cd ../../app/src/SampleCrud.Infra &&
dotnet ef --startup-project ../SampleCrud.API/ migrations add Init
dotnet ef --startup-project ../SampleCrud.API/ database update;

rm -rf Migrations;