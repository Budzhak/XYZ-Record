# Сторонние компоненты

## FFmpeg

Папка `ffmpeg\` содержит FFmpeg и библиотеки из сборки, поставляемой с видеоредактором [Shotcut](https://shotcut.org) (взято из `C:\Program Files\Shotcut`).

- Версия: `ffmpeg n7.1.1-57-g1b48158a23` (gcc 15.2.0, MSYS2).
- Конфигурация сборки: `--enable-gpl --enable-version3 --enable-shared --enable-libx264 --enable-libx265 --enable-libvpx --enable-libopus --enable-libmp3lame --enable-libvorbis --enable-libtheora --enable-libvpl --enable-libdav1d --enable-libaom --enable-libsvtav1 --enable-libwebp --enable-libzimg --enable-libvmaf`.
- Лицензия этой сборки: **GNU GPL v3** (из-за `--enable-gpl --enable-version3` и libx264/libx265). Сама библиотека FFmpeg — LGPL 2.1+/GPL в зависимости от параметров сборки.
- Сайт: https://ffmpeg.org — исходники: https://ffmpeg.org/download.html#get-sources и https://git.ffmpeg.org/ffmpeg.git
- Исходники сборки Shotcut: https://github.com/mltframework/shotcut

Состав (37 файлов, ≈ 88 МБ): ffmpeg.exe, ffprobe.exe, avcodec-61, avdevice-61, avfilter-10, avformat-61, avutil-59, postproc-58, swresample-5, swscale-8 (FFmpeg, LGPL/GPL); libx264-164 (GPL-2.0+), libx265-215 (GPL-2.0+), libvpx-1 (BSD), libaom (BSD-2), libdav1d (BSD-2), libSvtAv1Enc-3 (BSD-3-Clause-Clear), libopus-0 (BSD), libvorbis-0/libvorbisenc-2/libogg-0 (BSD), libtheoradec-2/libtheoraenc-2 (BSD), libmp3lame-0 (LGPL-2.0+), libwebp-7/libwebpmux-3/libsharpyuv-0 (BSD), libvpl-2 (MIT), libzimg-2 (WTFPL), libvmaf (BSD-2-Clause-Patent), SDL2 (zlib), zlib1 (zlib), libbz2-1 (bzip2), liblzma-5 (0BSD / public domain), libiconv-2 (LGPL-2.1+), libwinpthread-1 (MIT/BSD), libgcc_s_seh-1 и libstdc++-6 (GPL-3.0 с исключением GCC Runtime Library).

Компоненты распространяются без изменений; их исходные тексты доступны по ссылкам выше.

## Шрифты

Программа использует системные шрифты Windows (Bahnschrift, Segoe UI) и не включает их в комплект.
