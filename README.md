# CSharpStudy
시작하세요 C# 8.0 프로그래밍 책 기반

## 20200620
- 클래스 라이브러리 프로젝트를 생성 후 DeleteFileTask.cs 파일 이동 <br>
- 기존 실행 프로젝트를 참조하여 DeleteFileTask를 구현 <br>
- 아래 위치에 대한 코드 변경이 필요하다 <br>
> Program.cs InterfaceCallback 메서드
<pre>
<code>
Assembly assembly = Assembly.GetEntryAssembly();
Type type = assembly.GetType(classFullName);
object obj = Activator.CreateInstance(type);
</code>
</pre>
