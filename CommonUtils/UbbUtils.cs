using System.Text.RegularExpressions;

namespace CommonUtils
{
    /// <summary>
    /// ubb 处理工具类
    /// </summary>
    public class UbbUtils
    {
        #region 正则表达式
        /// <summary>
        /// ubb
        /// </summary>
        private static Regex encodeRegex = new Regex("\\[(img|align|color|url|b|font|size)[\\s]*(=.*?)?\\](.*?)\\[/\\1\\]", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        /// <summary>
        /// ubb end
        /// </summary>
        private static Regex encodeEndRegex = new Regex("\\[/[\\s]*(img|align|color|url|b|p|div|h1|h2|h3|h4|h5|h6|table|tr|td|ul|li|i|u|size|font)[\\s]*\\]", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        /// <summary>
        /// img 1
        /// </summary>
        private static Regex encodeImgRegex = new Regex("\\[img[\\s]*\\](.*?)\\[/img\\]", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        /// <summary>
        /// align
        /// </summary>
        private static Regex encodeAlignRegex = new Regex("\\[align[\\s]*=(.*?)\\]", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        /// <summary>
        /// color
        /// </summary>
        private static Regex encodeColorRegex = new Regex("\\[color[\\s]*=(.*?)\\]", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        /// <summary>
        /// url
        /// </summary>
        private static Regex encodeUrlRegex = new Regex("\\[url[\\s]*=(.*?)\\]", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        /// <summary>
        /// b
        /// </summary>
        private static Regex encodeBRegex = new Regex("\\[b[\\s]*\\]", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        /// <summary>
        /// size
        /// </summary>
        private static Regex encodeSizeRegex = new Regex("\\[size[\\s]*=(.*?)\\]", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        /// <summary>
        /// font  
        /// </summary>
        private static Regex encodeFontRegex = new Regex("\\[font[\\s]*=(.*?)\\]", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        /// <summary>
        /// [combinelink=sourceid##combineuname=" + uName + "##combineismy=" + isMyCom + "]
        /// </summary>
        private static Regex combieFontRegex = new Regex("\\[combinelink[\\s]*=(.*?)##combineuname[\\s]*=(.*?)\\]", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        /// <summary>
        /// [combinelink=sourceid##combineuname=" + uName + "##combineorder=" + order + "]
        /// </summary>
        private static Regex combieFontRegex2 = new Regex("\\[combinelink[\\s]*=(.*?)##combineuname[\\s]*=(.*?)##combineorder[\\s]*=(.*?)\\]", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        /// <summary>
        /// [zuhelianjie=sourceid##zuhemingcheng=" + uName + "##combineismy=" + isMyCom + "]
        /// </summary>
        private static Regex zuheRegex = new Regex("\\[zuhelianjie[\\s]*=(.*?)##zuhemingcheng[\\s]*=(.*?)\\]", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        /// <summary>
        /// html
        /// </summary>
        public static Regex decodeRegex = new Regex("<img.*?src=\"(http://.*?)\".*?/>|<p.*?text-align:[\\s]*(left|center|right|justify);\".*?>(.*?)</p>|<span.*?color:[\\s]*(.+?)\".*?>(.*?)</span>|<a.*?href=[\'\"\\s]*([^\\s\'\"]*)(http://.*?)*\">(.*?)</a>|<strong>(.*?)</strong>", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        /// <summary>
        /// 块级元素
        /// </summary>
        private static Regex encodeBlockRegex = new Regex("\\[[\\s]*(p|div|h1|h2|h3|h4|h5|h6|table|tr|td|ul|li|i|u)[\\s]*\\]", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        #endregion

        #region 将ubb 转换为html

        /// <summary>
        /// 将ubb 转换为html
        /// </summary>
        /// <param name="content"></param>
        /// <param name="hight"></param>
        /// <returns></returns>
        public static string EncodeUbb(string content)
        {
            if (string.IsNullOrEmpty(content)) return content;
            //string result = encodeRegex.Replace(content, match => EncodeFormatter(match, content));
            //string result = blogTimeReg.Replace(value, "<span style=\"display:block;color: #8c8c8c;margin-top: 16px;margin-bottom: 8px;font-size: 13px;\">$1  作者更新以下内容</span>");
            content = encodeImgRegex.Replace(content, "<img src=\"$1\" />");
            //对其
            content = encodeAlignRegex.Replace(content, "<p  style=\"text-align:$1;\">");
            //颜色
            content = encodeColorRegex.Replace(content, "<span style=\"color:$1;\">");
            //超链接
            content = encodeUrlRegex.Replace(content, "<a  href=\"$1\">");
            //加粗
            content = encodeBRegex.Replace(content, "<strong>");
            //字体
            content = encodeFontRegex.Replace(content, "<font style=\"font-family:$1;\">");
            //大小
            content = encodeSizeRegex.Replace(content, "<font  style=\"font-size:$1px;\">");
            //组合
            content = combieFontRegex.Replace(content, "【$2】");
            content = combieFontRegex2.Replace(content, "【$2】");
			content = zuheRegex.Replace(content, "【$2】");
            //块级元素
            content = encodeBlockRegex.Replace(content, "<$1>");
            //替换ubb 结束符
            content = encodeEndRegex.Replace(content, match => EncodeEndFormatter(match, content));

            return content;
        }

        /// <summary>
        /// 格式定位转换
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private static string EncodeEndFormatter(Match m, string content)
        {
            try
            {
                //匹配串
                string value = m.Value;
                //标签类型
                string tag = m.Groups[1].ToString().ToLower();
                switch (tag)
                {
                    case "url"://a 标签
                        {
                            return "</a>";
                        }
                        break;
                    case "align"://对齐
                        {
                            return "</p>";
                        }
                        break;
                    case "color"://颜色
                        {
                            return "</span>";
                        }
                        break;
                    case "b"://加粗
                        {
                            return "</strong>";
                        }
                        break;
                    case "size":
                    case "font":
                        {
                            return "</font>";
                        }
                        break;
                    default:
                        {
                            return "</" + tag + ">";
                        }
                        break;
                }
            }
            catch
            {
            }
            return m.Value;
        }

        /// <summary>
        /// 格式定位转换
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private static string EncodeFormatter(Match m, string content)
        {
            try
            {
                //匹配串
                string value = m.Value;
                //标签类型
                string tag = m.Groups[1].ToString().ToLower();
                //标签值
                string tagvalue = m.Groups[2].ToString();
                if (!string.IsNullOrEmpty(tagvalue) && tagvalue.StartsWith("="))
                {
                    tagvalue = tagvalue.Substring(1);
                }
                //标签内容
                string tagcontent = m.Groups[3].ToString();
                switch (tag)
                {
                    case "url"://a 标签
                        {
                            if (!string.IsNullOrEmpty(tagvalue))
                            {
                                return "<a  href=\"" + tagvalue + " \">" + EncodeUbb(tagcontent) + "</a>";
                            }
                        }
                        break;
                    case "img"://图片
                        {
                            if (!string.IsNullOrEmpty(tagcontent))
                            {
                                return "<img src=\"" + EncodeUbb(tagcontent) + "\" />";
                            }
                        }
                        break;
                    case "align"://对齐
                        {
                            if (!string.IsNullOrEmpty(tagvalue))
                            {
                                return "<p  style=\"text-align:" + tagvalue + ";\">" + EncodeUbb(tagcontent) + "</p>";
                            }
                        }
                        break;
                    case "color"://颜色
                        {
                            if (!string.IsNullOrEmpty(tagvalue))
                            {
                                return "<span style=\"color:" + tagvalue + ";\">" + EncodeUbb(tagcontent) + "</span>";
                            }
                        }
                        break;
                    case "b"://加粗
                        {
                            if (!string.IsNullOrEmpty(tagcontent))
                            {
                                return "<strong>" + EncodeUbb(tagcontent) + "</strong>";
                            }
                        }
                        break;
                    default:
                        {

                        }
                        break;
                }
                return tagcontent;
            }
            catch
            {
            }
            return m.Value;
        }

        #endregion

        #region 将html 反解为ubb
        /// <summary>
        /// 将html 反解为ubb
        /// </summary>
        /// <returns></returns>
        public static string DecodeUbb(string content)
        {
            if (string.IsNullOrEmpty(content)) return content;
            string result = decodeRegex.Replace(content, match => DecodeFormatter(match, content));
            return result;
        }

      
        private static string DecodeFormatter(Match m, string content)
        {
            try
            {
                string value = m.Value;
                if (value.StartsWith("<img"))
                {
                    string properyValue = m.Groups[1].ToString();
                    if (!string.IsNullOrEmpty(properyValue) && properyValue.StartsWith("http://gbres.dfcfw.com/") && (properyValue.ToLower().EndsWith(".jpg")
                        || properyValue.ToLower().EndsWith("png") || properyValue.ToLower().EndsWith("gif") || properyValue.ToLower().EndsWith("bmp")))
                    {
                        return "[img]" + properyValue + "[/img]";
                    }
                }
                else if (value.StartsWith("<p"))
                {
                    string properyValue = m.Groups[2].ToString();
                    if (!string.IsNullOrEmpty(properyValue))
                    {
                        return "[align=" + properyValue + "]" + m.Groups[3].ToString() + "[/align]";
                    }
                }
                else if (value.StartsWith("<span"))
                {
                    string properyValue = m.Groups[4].ToString();
                    if (!string.IsNullOrEmpty(properyValue))
                    {
                        return "[color=" + properyValue + "]" + m.Groups[5].ToString() + "[/color]";
                    }
                }
                else if (value.StartsWith("<a"))
                {
                    string href = m.Groups[7].ToString();
                    if (!string.IsNullOrEmpty(href))
                    {
                        return "[url=" + href + "]" + m.Groups[8].ToString() + "[/url]";
                    }
                }
                else if (value.StartsWith("<strong"))
                {

                    return "[b]" + m.Groups[9].ToString() + "[/b]";
                }
                return value;
            }
            catch
            {
            }
            return m.Value;
        }
        #endregion


        private static string FormatAtGubaPicUbb(Match m)
        {
            string value = m.Value;
            try
            {
                string href = "";
                Match hrefResult = encodeImgRegex.Match(value);
                if (hrefResult.Groups.Count > 1)
                {
                    href = hrefResult.Groups[1].ToString();
                }

                if (href.IndexOf("https://gbres.dfcfw.com/Files") == 0 || href.IndexOf("https://pifm3.eastmoney.com") == 0)
                {
                    return value;
                }
                else
                {
                    return "";
                }

            }
            catch
            {
                return "";
            }
        }
        public static string ChangeImgNotGubaUbbToEmpty(string content)
        {
            content = encodeImgRegex.Replace(content, match => FormatAtGubaPicUbb(match));
            return content;
        }


        /// <summary>
        /// img 1
        /// </summary>
        private static Regex encodeImgRegexHtml = new Regex("\\<img[\\s]*\\>(.*?)\\</img\\>", RegexOptions.IgnoreCase | RegexOptions.Singleline);

        private static string ChangeImgToEmpty(Match m)
        {
            string value = m.Value;
            try
            {
                string href = "";
                Match hrefResult = encodeImgRegexHtml.Match(value);
                if (hrefResult.Groups.Count > 1)
                {
                    href = hrefResult.Groups[1].ToString();
                }

                if (href.IndexOf("http://gbres.dfcfw.com/Files") == 0 || href.IndexOf("http://pifm3.eastmoney.com") == 0)
                {
                    return "<img src=\"" + href + "\" />";
                }
                else
                {
                    return "";
                }

            }
            catch
            {
                return "";
            }
        }
        public static string ChangeImgNotGubaToEmpty(string content)
        {
            content = encodeImgRegexHtml.Replace(content, match => ChangeImgToEmpty(match));
            return content;
        }
    }
}
