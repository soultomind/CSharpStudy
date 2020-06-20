# CSharpStudy
C# 기초 스터디입니다.

## 20200620
DeleteFileTask 클래스를 따로 클래스 라이브러리 프로젝트를 생성 후에 추가하여 기존 실행 프로젝트를 참조하여 DeleteFileTask를 구현
Program.cs InterfaceCallback 메서드 해당 위치에 대한 코드 변경이 필요하다
<pre>
<code>
Assembly assembly = Assembly.GetEntryAssembly();
Type type = assembly.GetType(classFullName);
object obj = Activator.CreateInstance(type);
</code>
</pre>
