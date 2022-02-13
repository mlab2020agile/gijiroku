# gijiroku

## ディレクトリ構造

-  New Unity Project
    - 草間くんのテスト用テストプロジェクト
- Speech Minutes 2020
    - 議事録ツール本体のプロジェクト

## gitignoreについて
[こちら](https://www.cg-method.com/unity-gitignore-setting/　 )のサイトを参考にignoreを設定した

## Unityのバージョンについて
- Unityのバージョンは2018.4.23.f1を使用

## テストケースリンク
- https://docs.google.com/spreadsheets/d/1Cpis457mzLgTDf4hvMrtnZZFkvcvk0h0/edit#gid=1926496207

##ボイスチャットと音声認識が同時に使えない問題
-ボイスチャットをしながら音声認識を行うと、そのタイミングでマイクが音声認識に移り、マイク入力の更新が途絶えてしまうため、一つのアプリケーション内でボイスチャットと音声認識を同時に使用することができない。
-改善案:1つのPCでアプリを２つ同時に開くことで、ボイスチャットと音声認識を同時に使うことが可能。
