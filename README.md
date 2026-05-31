Лабораторная работа 7. Анализ и преобразование кода с использованием Clang и LLVM
---
Описание работы  
Цель работы  
Познакомиться с инструментарием Clang и LLVM, освоить получение абстрактного синтаксического дерева (AST) и промежуточного представления (LLVM IR) для кода на C/C++, научиться применять базовые оптимизации, строить графы потока управления (CFG), а также анализировать влияние оптимизаций на различные синтаксические конструкции языка.  

Постановка задачи  
Необходимо выполнить следующие шаги:  

Установка среды  
Установить Clang, LLVM, opt и Graphviz (например, в Ubuntu 26.04).  

Работа с AST  
Сгенерировать абстрактное синтаксическое дерево для заданного C/C++‑файла.  

Генерация LLVM IR  
Получить промежуточное представление кода без оптимизаций (-O0) и с оптимизациями (-O2).  
  
Оптимизация IR  
Применить оптимизации с помощью opt и/или флагов Clang, сравнить изменения.  

Построение CFG  
Построить граф потока управления для одной или нескольких функций.  

Индивидуальное задание (по варианту)  
Выполнить анализ конкретной синтаксической конструкции в соответствии с вариантом. Сформулировать, как LLVM обрабатывает выбранную конструкцию, какие оптимизации применяются.  

Выводы  
Ответить на контрольные вопросы  

Общее задание  
1. Исходный код  
<img width="254" height="235" alt="image" src="https://github.com/user-attachments/assets/36659464-663a-429f-b4ea-3ac674ab7da2" />  

2. Работа с AST  
<img width="960" height="489" alt="image" src="https://github.com/user-attachments/assets/7070b771-bf6f-4abe-908a-810c3853d845" />  

3. Генерация LLVM IR
```llvm
; ModuleID = 'main.c'
source_filename = "main.c"
target datalayout = "e-m:e-p270:32:32-p271:32:32-p272:64:64-i64:64-i128:128-f80:128-n8:16:32:64-S128"
target triple = "x86_64-pc-linux-gnu"

@.str = private unnamed_addr constant [4 x i8] c"%d\0A\00", align 1

; Function Attrs: noinline nounwind optnone uwtable
define dso_local i32 @square(i32 noundef %0) #0 {
  %2 = alloca i32, align 4
  store i32 %0, ptr %2, align 4
  %3 = load i32, ptr %2, align 4
  %4 = load i32, ptr %2, align 4
  %5 = mul nsw i32 %3, %4
  ret i32 %5
}

; Function Attrs: noinline nounwind optnone uwtable
define dso_local i32 @main() #0 {
  %1 = alloca i32, align 4
  %2 = alloca i32, align 4
  %3 = alloca i32, align 4
  store i32 0, ptr %1, align 4
  store i32 5, ptr %2, align 4
  %4 = load i32, ptr %2, align 4
  %5 = call i32 @square(i32 noundef %4)
  store i32 %5, ptr %3, align 4
  %6 = load i32, ptr %3, align 4
  %7 = call i32 (ptr, ...) @printf(ptr noundef @.str, i32 noundef %6)
  ret i32 0
}

declare i32 @printf(ptr noundef, ...) #1

attributes #0 = { noinline nounwind optnone uwtable "frame-pointer"="all" "min-legal-vector-width"="0" "no-trapping-math"="true" "stack-protector-buffer-size"="8" "target-cpu"="x86-64" "target-features"="+cmov,+cx8,+fxsr,+mmx,+sse,+sse2,+x87" "tune-cpu"="generic" }
attributes #1 = { "frame-pointer"="all" "no-trapping-math"="true" "stack-protector-buffer-size"="8" "target-cpu"="x86-64" "target-features"="+cmov,+cx8,+fxsr,+mmx,+sse,+sse2,+x87" "tune-cpu"="generic" }

!llvm.module.flags = !{!0, !1, !2, !3, !4}
!llvm.ident = !{!5}

!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 8, !"PIC Level", i32 2}
!2 = !{i32 7, !"PIE Level", i32 2}
!3 = !{i32 7, !"uwtable", i32 2}
!4 = !{i32 7, !"frame-pointer", i32 2}
!5 = !{!"Ubuntu clang version 21.1.8 (6ubuntu1)"}
```
4. Оптимизация IR
main_O0.ll
```llvm
; ModuleID = 'main.c'
source_filename = "main.c"
target datalayout = "e-m:e-p270:32:32-p271:32:32-p272:64:64-i64:64-i128:128-f80:128-n8:16:32:64-S128"
target triple = "x86_64-pc-linux-gnu"

@.str = private unnamed_addr constant [4 x i8] c"%d\0A\00", align 1

; Function Attrs: noinline nounwind optnone uwtable
define dso_local i32 @square(i32 noundef %0) #0 {
  %2 = alloca i32, align 4
  store i32 %0, ptr %2, align 4
  %3 = load i32, ptr %2, align 4
  %4 = load i32, ptr %2, align 4
  %5 = mul nsw i32 %3, %4
  ret i32 %5
}

; Function Attrs: noinline nounwind optnone uwtable
define dso_local i32 @main() #0 {
  %1 = alloca i32, align 4
  %2 = alloca i32, align 4
  %3 = alloca i32, align 4
  store i32 0, ptr %1, align 4
  store i32 5, ptr %2, align 4
  %4 = load i32, ptr %2, align 4
  %5 = call i32 @square(i32 noundef %4)
  store i32 %5, ptr %3, align 4
  %6 = load i32, ptr %3, align 4
  %7 = call i32 (ptr, ...) @printf(ptr noundef @.str, i32 noundef %6)
  ret i32 0
}

declare i32 @printf(ptr noundef, ...) #1

attributes #0 = { noinline nounwind optnone uwtable "frame-pointer"="all" "min-legal-vector-width"="0" "no-trapping-math"="true" "stack-protector-buffer-size"="8" "target-cpu"="x86-64" "target-features"="+cmov,+cx8,+fxsr,+mmx,+sse,+sse2,+x87" "tune-cpu"="generic" }
attributes #1 = { "frame-pointer"="all" "no-trapping-math"="true" "stack-protector-buffer-size"="8" "target-cpu"="x86-64" "target-features"="+cmov,+cx8,+fxsr,+mmx,+sse,+sse2,+x87" "tune-cpu"="generic" }

!llvm.module.flags = !{!0, !1, !2, !3, !4}
!llvm.ident = !{!5}

!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 8, !"PIC Level", i32 2}
!2 = !{i32 7, !"PIE Level", i32 2}
!3 = !{i32 7, !"uwtable", i32 2}
!4 = !{i32 7, !"frame-pointer", i32 2}
!5 = !{!"Ubuntu clang version 21.1.8 (6ubuntu1)"}
```
main_O2.ll
```llvm
; ModuleID = 'main.c'
source_filename = "main.c"
target datalayout = "e-m:e-p270:32:32-p271:32:32-p272:64:64-i64:64-i128:128-f80:128-n8:16:32:64-S128"
target triple = "x86_64-pc-linux-gnu"

@.str = private unnamed_addr constant [4 x i8] c"%d\0A\00", align 1

; Function Attrs: mustprogress nofree norecurse nosync nounwind willreturn memory(none) uwtable
define dso_local i32 @square(i32 noundef %0) local_unnamed_addr #0 {
  %2 = mul nsw i32 %0, %0
  ret i32 %2
}

; Function Attrs: nofree nounwind uwtable
define dso_local noundef i32 @main() local_unnamed_addr #1 {
  %1 = tail call i32 (ptr, ...) @printf(ptr noundef nonnull dereferenceable(1) @.str, i32 noundef 25)
  ret i32 0
}

; Function Attrs: nofree nounwind
declare noundef i32 @printf(ptr noundef readonly captures(none), ...) local_unnamed_addr #2

attributes #0 = { mustprogress nofree norecurse nosync nounwind willreturn memory(none) uwtable "min-legal-vector-width"="0" "no-trapping-math"="true" "stack-protector-buffer-size"="8" "target-cpu"="x86-64" "target-features"="+cmov,+cx8,+fxsr,+mmx,+sse,+sse2,+x87" "tune-cpu"="generic" }
attributes #1 = { nofree nounwind uwtable "min-legal-vector-width"="0" "no-trapping-math"="true" "stack-protector-buffer-size"="8" "target-cpu"="x86-64" "target-features"="+cmov,+cx8,+fxsr,+mmx,+sse,+sse2,+x87" "tune-cpu"="generic" }
attributes #2 = { nofree nounwind "no-trapping-math"="true" "stack-protector-buffer-size"="8" "target-cpu"="x86-64" "target-features"="+cmov,+cx8,+fxsr,+mmx,+sse,+sse2,+x87" "tune-cpu"="generic" }

!llvm.module.flags = !{!0, !1, !2, !3}
!llvm.ident = !{!4}

!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 8, !"PIC Level", i32 2}
!2 = !{i32 7, !"PIE Level", i32 2}
!3 = !{i32 7, !"uwtable", i32 2}
!4 = !{!"Ubuntu clang version 21.1.8 (6ubuntu1)"}
```
5. Сравнение оптимизаций
<img width="772" height="713" alt="image" src="https://github.com/user-attachments/assets/68d4e73a-ef49-4605-aede-71426c0b7d25" />  
Изменения после оптимизации:  

1. Переменные типа alloca были удалены;  
2. Код переведён в SSA-форму;  
3. Оптимизация улучшила читаемость и упростила поток управления.  

6. Построение CFG для оптимизированного LLVM IR  
<img width="735" height="171" alt="image" src="https://github.com/user-attachments/assets/045d1174-b77f-4283-ace5-85ac6ea9dab1" />
<img width="339" height="172" alt="image" src="https://github.com/user-attachments/assets/a65d7252-a6b3-4a54-9bbe-8f2b2f8d6ce6" />

Индивидуальное задание
---
```cpp
inline int square(int x) {
return x * x;
}
int main() {
int a = 5;
int b = square(a);
return b;
}
```

Задания:  
Получите IR для -O0.  
Получите IR для -O2. Встроилась ли функция?  
Примените -always-inline и сравните с предыдущими  оптимизациями (у меня не работает этот флаг).  
Постройте CFG до и после.    
Сделайте вывод об условиях встраивания функций в LLVM.  
  
  1. IR до оптимизации -O0  
  <img width="1394" height="858" alt="11" src="https://github.com/user-attachments/assets/42c57c96-7728-4369-bddb-9f7006068426" />

  2. IR после оптимизации -O2  
  <img width="1382" height="526" alt="22" src="https://github.com/user-attachments/assets/10754c13-c9ca-42e5-9e5b-27342769c4fa" />  
  Функция встроилась — нет вызова call @square

  Свертка констант — square(5) вычислено в 25 во время компиляции

  Удалены все alloca — ненужные переменные удалены

  Упрощен CFG — один базовый блок с одним ret

  4. CFG. Слево до оптимизации, справо после.    
  <img width="1111" height="495" alt="33" src="https://github.com/user-attachments/assets/1ed9e9c2-dd44-479d-ac96-ff73ab23158e" />

Вывод: LLVM встраивает функции, когда:  

Уровень оптимизации ≥ -O2  

Функция не слишком большая (эвристика)  

Это безопасно (нет побочных эффектов, нет рекурсии)  

Это выгодно (уменьшает накладные расходы на вызов)  

Ответы на контрольные вопросы
---  
# 1. Что такое Clang, и какова его роль в процессе компиляции программ?  
Clang — это фронтенд компилятора для языков C, C++ и Objective-C. Его задача — преобразовать исходный код программы в промежуточное представление LLVM IR. Он выполняет синтаксический и семантический анализ, проверку типов и генерацию IR-кода.  

# 2. Что представляет собой LLVM и как он используется в современных компиляторах?  
LLVM — это инфраструктура для построения компиляторов, основанная на использовании универсального промежуточного представления (IR). Современные компиляторы (например, Clang) используют LLVM для оптимизации кода и генерации машинного кода под разные архитектуры. LLVM позволяет разделить компиляцию на три этапа: фронтенд (генерация IR), оптимизации (работа с IR) и бэкенд (генерация машинного кода).  

# 3. Чем отличается абстрактное синтаксическое дерево (AST) от промежуточного представления LLVM IR?  
AST отражает синтаксическую структуру исходного кода (условия, циклы, вызовы функций) и привязан к конкретному языку. LLVM IR — это низкоуровневое представление, близкое к ассемблеру, но платформонезависимое. AST используется на этапе анализа, IR — на этапе оптимизации и генерации кода.  

# 4. Для чего необходимо промежуточное представление (IR) в процессе компиляции?  
IR служит "мостом" между фронтендом и бэкендом компилятора. Оно позволяет:  

применять оптимизации независимо от исходного языка и целевой платформы;  

переиспользовать один и тот же оптимизатор и бэкенд для разных языков;  

упростить анализ и преобразование кода.  

# 5. Что делает инструкция alloca в LLVM IR, и зачем она используется в функциях?  
Инструкция alloca выделяет память на стеке для локальной переменной. Она используется на низких уровнях оптимизации (-O0), чтобы каждая переменная имела свой "домик" в памяти. Это упрощает отладку, но создаёт много лишних операций загрузки и сохранения. На высоких уровнях оптимизации alloca часто удаляются, а переменные помещаются в регистры.  

# 6. Зачем нужна оптимизация кода в компиляторе, и какие основные цели она преследует?  
Оптимизация улучшает качество генерируемого кода без изменения его поведения. Основные цели:  

ускорение работы программы;  

уменьшение размера исполняемого файла;  

снижение энергопотребления;  

удаление мёртвого и недостижимого кода.  

# 7. Что такое SSA-форма и почему она важна при оптимизации программ?  
SSA (Static Single Assignment) — форма представления кода, в которой каждая переменная получает значение ровно один раз. Это упрощает многие оптимизации: анализ потока данных, удаление мёртвого кода, постоянное распространение. В LLVM IR по умолчанию используется SSA-форма.  

# 8. Что такое граф потока управления (CFG) и как он помогает анализировать поведение программы?  
CFG (Control Flow Graph) — это ориентированный граф, вершины которого — базовые блоки, а рёбра — переходы между ними. CFG помогает анализировать порядок выполнения инструкций, находить недостижимый код, оптимизировать циклы и условия. В вашей работе CFG наглядно показывает, как оптимизация превращает несколько блоков в один.  

# 9. Как устроено представление арифметических операций в LLVM IR (например, умножение, сложение)?  
Арифметические операции в LLVM IR записываются в виде:  

```
%результат = mul i32 %a, %b   ; умножение
%результат = add i32 %a, %b   ; сложение
```  
Обязательно указывается тип операндов (i32, i64, float и т.д.).  
# 10. Почему функции в LLVM IR обычно представляют собой отдельные единицы анализа и оптимизации?  
Функции — это естественные границы видимости переменных и области действия оптимизаций. Оптимизатор может анализировать и преобразовывать код внутри функции независимо от других функций. Это упрощает алгоритмы и позволяет выполнять оптимизации, такие как встраивание и удаление мёртвого кода.  

# 11. Что происходит с функцией в LLVM IR, если она вызывается один раз и очень короткая?  
На уровне оптимизаций -O2 и выше такая функция, скорее всего, будет встроена (inlined). Вместо вызова call компилятор подставит тело функции в место вызова. В вашем примере функция square вызвалась один раз и содержала одно умножение. После встраивания и последующих оптимизаций от функции не осталось и следа — весь код превратился в ret i32 25.  

# 12. Какие преимущества даёт использование IR и CFG для автоматических оптимизаций по сравнению с анализом исходного текста на C?  
Единообразие — IR единообразен для всех языков, оптимизации пишутся один раз.  

Простота анализа — в IR уже выполнены проверка типов и разрешение имён.  

Удобство преобразований — IR легко перестраивать (удалять, вставлять, заменять инструкции).  

CFG даёт явную структуру потока управления, что позволяет точно анализировать порядок выполнения.  

Анализ исходного C-кода сложнее из-за макросов, указателей, сложных выражений и семантики языка.   

Дополнительное задание.
---
Вариант работы - 78 (Создание функции языка C/C++)  
Верный пример:   
```
int exp() {  
return 12+(2*12)+1*1*1*2;
}
```  
Оптимизация №1 Свёртка констант  
Вычисляет на этапе компиляции выражения, все операнды которых являются константами (например, 2 + 3 заменяется на 5). В контексте трёхадресного кода оптимизатор ищет арифметические инструкции (ADD, SUB, MUL, DIV), у которых оба аргумента – числа (либо непосредственные литералы, либо временные переменные, которым ранее было присвоено константное значение). Затем он вычисляет результат и заменяет исходную инструкцию на присваивание результата (ASSIGN). Это уменьшает количество инструкций и ускоряет выполнение.  
Оптимизация №2 Упрощение арифметики  
Заменяет операции с нейтральными или поглощающими элементами на более простые формы.  
Тестовый пример  
<img width="692" height="208" alt="image" src="https://github.com/user-attachments/assets/1690bf34-5aeb-43d2-9a27-5eaaad93343c" />  
<img width="724" height="221" alt="image" src="https://github.com/user-attachments/assets/0570b780-46e7-4898-bd52-018f8ea17b52" />  
<img width="621" height="173" alt="image" src="https://github.com/user-attachments/assets/d671999e-d52b-479f-8e4a-4a778aae3d74" />    
<img width="686" height="201" alt="image" src="https://github.com/user-attachments/assets/c277e435-499c-45e5-8ea8-a065c102fb6a" />  
Блок-схема оптимизации №1  
<img width="830" height="619" alt="image" src="https://github.com/user-attachments/assets/e6d7e587-00a6-4108-98dc-0f784c6b4fc4" />  
Блок-схема оптимизации №2  
<img width="643" height="489" alt="image" src="https://github.com/user-attachments/assets/9e47467b-2970-46bc-94d6-d3fd347f1cc8" />  



