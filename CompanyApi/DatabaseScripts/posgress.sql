CALL create_employee(
               'Сергей Вячеславович Лазарев',
               'lazarev@artzvezdy.ru',
               '+79857731755',
               3,
               'lazarev',
               'oq{Na?%BnEs%');


CALL create_employee(
               'Иванова Айша Станиславовна',
               'ivanova@mail.ru',
               '+79942497578',
               1,
               'ivanova',
               'a$2XLtNHK5Gm');

CALL create_employee(
               'Исаева Мария Никитична',
               'isaeva@mail.ru',
               '+79386832616',
               2,
               'isaeva',
               'BitQky$}QTgr');

CALL create_employee(
               'Голубева Софья Тимофеевна',
               'golubeva@mail.ru',
               '+79249267433',
               2,
               'golubeva',
               '{c8f4ZwRk~$O');

CALL create_employee(
               'Панин Артём Матвеевич',
               'panin@mail.ru',
               '+79972415610',
               2,
               'panin',
               't{H07HwqUop{');

CALL create_employee(
               'Суркова Василиса Максимовна',
               'surkiva@mail.ru',
               '+79515070566',
               1,
               'surkiva',
               'YTt%5#Rynb$');

CREATE ROLE worker;
CREATE ROLE manager;
CREATE ROLE admin_c;

GRANT SELECT ON
    connection_interface_type,
    contact_person,
    contract,
    contract_classifier,
    drive_type,
    hard_drive,
    organization,
    organization_type,
    priority_task,
    task,
    task_receipt_type,
    task_status
    TO worker, manager;

GRANT SELECT ON
    employee
    TO worker, manager;

GRANT INSERT ON
contact_person,
    organization,
    contract,
    task
    TO manager;

GRANT UPDATE
    (executor_id)
    ON task
    TO manager;
/* Менеджер  может изменять  исполнителя */

ALTER TABLE
    task
    ENABLE ROW LEVEL SECURITY;

CREATE POLICY select_of_task ON task
    FOR SELECT
                   TO manager, worker
                   USING
                   (
                   (SELECT employee_login
                   FROM employee
                   WHERE task.author_id = employee.employee_id) = current_user OR
                   (SELECT employee_login
                   FROM employee
                   WHERE task.executor_id = employee.employee_id) = current_user
                   );
/* Просматривать задание может лиюо автор, либо исполнитель */

CREATE POLICY update_task_status ON task
    FOR UPDATE
                   TO manager, worker
                   USING (true)
        WITH CHECK
                   (
                   (task.status_id != 3) AND
                   (
                   (SELECT employee_login
                   FROM employee
                   WHERE (task.author_id = employee.employee_id)) = current_user OR
                   (SELECT employee_login
                   FROM employee
                   WHERE (task.executor_id = employee.employee_id)) = current_user
                   )
                   );
/* Изменять задание как выполненое может только автор или исполнитель (выполененое задание изменять нельзя) */
