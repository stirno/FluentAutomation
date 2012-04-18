using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenQA.Selenium;

namespace FluentAutomation
{
    public static class Sizzle
    {
        private static readonly string compiledSizzleSource = @"(function(){function C(a,b,c,d,f,e){for(var f=0,h=d.length;f<h;f++){var g=d[f];if(g){for(var i=!1,g=g[a];g;){if(g[p]===c){i=d[g.sizset];break}1===g.nodeType&&!e&&(g[p]=c,g.sizset=f);if(g.nodeName.toLowerCase()===b){i=g;break}g=g[a]}d[f]=i}}}function D(a,b,c,d,f,e){for(var f=0,h=d.length;f<h;f++){var g=d[f];if(g){for(var j=!1,g=g[a];g;){if(g[p]===c){j=d[g.sizset];break}if(1===g.nodeType)if(e||(g[p]=c,g.sizset=f),""string""!==typeof b){if(g===b){j=!0;break}}else if(0<i.filter(b,[g]).length){j=g;break}g=
g[a]}d[f]=j}}}var z=/((?:\((?:\([^()]+\)|[^()]+)+\)|\[(?:\[[^\[\]]*\]|['""][^'""]*['""]|[^\[\]'""]+)+\]|\\.|[^ >+~,(\[\\]+)+|[>+~])(\s*,\s*)?((?:.|\r|\n)*)/g,p=""sizcache""+(Math.random()+"""").replace(""."",""""),A=0,E=Object.prototype.toString,w=!1,F=!0,s=/\\/g,x=/\W/;[0,0].sort(function(){F=!1;return 0});var i=function(a,b,c,d){var c=c||[],f=b=b||document;if(1!==b.nodeType&&9!==b.nodeType)return[];if(!a||""string""!==typeof a)return c;var e,h,g,l,t,n=!0,o=i.isXML(b),k=[],q=a;do if(z.exec(""""),e=z.exec(q))if(q=
e[3],k.push(e[1]),e[2]){l=e[3];break}while(e);if(1<k.length&&G.exec(a))if(2===k.length&&j.relative[k[0]])h=H(k[0]+k[1],b,d);else for(h=j.relative[k[0]]?[b]:i(k.shift(),b);k.length;)a=k.shift(),j.relative[a]&&(a+=k.shift()),h=H(a,h,d);else if(!d&&1<k.length&&9===b.nodeType&&!o&&j.match.ID.test(k[0])&&!j.match.ID.test(k[k.length-1])&&(e=i.find(k.shift(),b,o),b=e.expr?i.filter(e.expr,e.set)[0]:e.set[0]),b){e=d?{expr:k.pop(),set:m(d)}:i.find(k.pop(),1===k.length&&(""~""===k[0]||""+""===k[0])&&b.parentNode?
b.parentNode:b,o);h=e.expr?i.filter(e.expr,e.set):e.set;for(0<k.length?g=m(h):n=!1;k.length;)e=t=k.pop(),j.relative[t]?e=k.pop():t="""",null==e&&(e=b),j.relative[t](g,e,o)}else g=[];g||(g=h);g||i.error(t||a);if(""[object Array]""===E.call(g))if(n)if(b&&1===b.nodeType)for(a=0;null!=g[a];a++)g[a]&&(!0===g[a]||1===g[a].nodeType&&i.contains(b,g[a]))&&c.push(h[a]);else for(a=0;null!=g[a];a++)g[a]&&1===g[a].nodeType&&c.push(h[a]);else c.push.apply(c,g);else m(g,c);l&&(i(l,f,c,d),i.uniqueSort(c));return c};
i.uniqueSort=function(a){if(y&&(w=F,a.sort(y),w))for(var b=1;b<a.length;b++)a[b]===a[b-1]&&a.splice(b--,1);return a};i.matches=function(a,b){return i(a,null,null,b)};i.matchesSelector=function(a,b){return 0<i(b,null,null,[a]).length};i.find=function(a,b,c){var d,f,e,h,g,i;if(!a)return[];f=0;for(e=j.order.length;f<e;f++)if(g=j.order[f],h=j.leftMatch[g].exec(a))if(i=h[1],h.splice(1,1),""\\""!==i.substr(i.length-1)&&(h[1]=(h[1]||"""").replace(s,""""),d=j.find[g](h,b,c),null!=d)){a=a.replace(j.match[g],"""");
break}d||(d=""undefined""!==typeof b.getElementsByTagName?b.getElementsByTagName(""*""):[]);return{set:d,expr:a}};i.filter=function(a,b,c,d){for(var f,e,h,g,l,m,n,o,k=a,q=[],r=b,p=b&&b[0]&&i.isXML(b[0]);a&&b.length;){for(h in j.filter)if(null!=(f=j.leftMatch[h].exec(a))&&f[2])if(m=j.filter[h],l=f[1],e=!1,f.splice(1,1),""\\""!==l.substr(l.length-1)){r===q&&(q=[]);if(j.preFilter[h])if(f=j.preFilter[h](f,r,c,q,d,p)){if(!0===f)continue}else e=g=!0;if(f)for(n=0;null!=(l=r[n]);n++)l&&(g=m(l,f,n,r),o=d^g,c&&null!=
g?o?e=!0:r[n]=!1:o&&(q.push(l),e=!0));if(void 0!==g){c||(r=q);a=a.replace(j.match[h],"""");if(!e)return[];break}}if(a===k)if(null==e)i.error(a);else break;k=a}return r};i.error=function(a){throw Error(""Syntax error, unrecognized expression: ""+a);};var B=i.getText=function(a){var b,c;b=a.nodeType;var d="""";if(b)if(1===b||9===b||11===b){if(""string""===typeof a.textContent)return a.textContent;for(a=a.firstChild;a;a=a.nextSibling)d+=B(a)}else{if(3===b||4===b)return a.nodeValue}else for(b=0;c=a[b];b++)8!==
c.nodeType&&(d+=B(c));return d},j=i.selectors={order:[""ID"",""NAME"",""TAG""],match:{ID:/#((?:[\w\u00c0-\uFFFF\-]|\\.)+)/,CLASS:/\.((?:[\w\u00c0-\uFFFF\-]|\\.)+)/,NAME:/\[name=['""]*((?:[\w\u00c0-\uFFFF\-]|\\.)+)['""]*\]/,ATTR:/\[\s*((?:[\w\u00c0-\uFFFF\-]|\\.)+)\s*(?:(\S?=)\s*(?:(['""])(.*?)\3|(#?(?:[\w\u00c0-\uFFFF\-]|\\.)*)|)|)\s*\]/,TAG:/^((?:[\w\u00c0-\uFFFF\*\-]|\\.)+)/,CHILD:/:(only|nth|last|first)-child(?:\(\s*(even|odd|(?:[+\-]?\d+|(?:[+\-]?\d*)?n\s*(?:[+\-]\s*\d+)?))\s*\))?/,POS:/:(nth|eq|gt|lt|first|last|even|odd)(?:\((\d*)\))?(?=[^\-]|$)/,
PSEUDO:/:((?:[\w\u00c0-\uFFFF\-]|\\.)+)(?:\((['""]?)((?:\([^\)]+\)|[^\(\)]*)+)\2\))?/},leftMatch:{},attrMap:{""class"":""className"",""for"":""htmlFor""},attrHandle:{href:function(a){return a.getAttribute(""href"")},type:function(a){return a.getAttribute(""type"")}},relative:{""+"":function(a,b){var c=""string""===typeof b,d=c&&!x.test(b),c=c&&!d;d&&(b=b.toLowerCase());for(var d=0,f=a.length,e;d<f;d++)if(e=a[d]){for(;(e=e.previousSibling)&&1!==e.nodeType;);a[d]=c||e&&e.nodeName.toLowerCase()===b?e||!1:e===b}c&&i.filter(b,
a,!0)},"">"":function(a,b){var c,d=""string""===typeof b,f=0,e=a.length;if(d&&!x.test(b))for(b=b.toLowerCase();f<e;f++){if(c=a[f])c=c.parentNode,a[f]=c.nodeName.toLowerCase()===b?c:!1}else{for(;f<e;f++)(c=a[f])&&(a[f]=d?c.parentNode:c.parentNode===b);d&&i.filter(b,a,!0)}},"""":function(a,b,c){var d,f=A++,e=D;""string""===typeof b&&!x.test(b)&&(d=b=b.toLowerCase(),e=C);e(""parentNode"",b,f,a,d,c)},""~"":function(a,b,c){var d,f=A++,e=D;""string""===typeof b&&!x.test(b)&&(d=b=b.toLowerCase(),e=C);e(""previousSibling"",
b,f,a,d,c)}},find:{ID:function(a,b,c){if(""undefined""!==typeof b.getElementById&&!c)return(a=b.getElementById(a[1]))&&a.parentNode?[a]:[]},NAME:function(a,b){if(""undefined""!==typeof b.getElementsByName){for(var c=[],d=b.getElementsByName(a[1]),f=0,e=d.length;f<e;f++)d[f].getAttribute(""name"")===a[1]&&c.push(d[f]);return 0===c.length?null:c}},TAG:function(a,b){if(""undefined""!==typeof b.getElementsByTagName)return b.getElementsByTagName(a[1])}},preFilter:{CLASS:function(a,b,c,d,f,e){a="" ""+a[1].replace(s,
"""")+"" "";if(e)return a;for(var e=0,h;null!=(h=b[e]);e++)h&&(f^(h.className&&0<=("" ""+h.className+"" "").replace(/[\t\n\r]/g,"" "").indexOf(a))?c||d.push(h):c&&(b[e]=!1));return!1},ID:function(a){return a[1].replace(s,"""")},TAG:function(a){return a[1].replace(s,"""").toLowerCase()},CHILD:function(a){if(""nth""===a[1]){a[2]||i.error(a[0]);a[2]=a[2].replace(/^\+|\s*/g,"""");var b=/(-?)(\d*)(?:n([+\-]?\d*))?/.exec(""even""===a[2]&&""2n""||""odd""===a[2]&&""2n+1""||!/\D/.test(a[2])&&""0n+""+a[2]||a[2]);a[2]=b[1]+(b[2]||1)-0;
a[3]=b[3]-0}else a[2]&&i.error(a[0]);a[0]=A++;return a},ATTR:function(a,b,c,d,f,e){b=a[1]=a[1].replace(s,"""");!e&&j.attrMap[b]&&(a[1]=j.attrMap[b]);a[4]=(a[4]||a[5]||"""").replace(s,"""");""~=""===a[2]&&(a[4]="" ""+a[4]+"" "");return a},PSEUDO:function(a,b,c,d,f){if(""not""===a[1])if(1<(z.exec(a[3])||"""").length||/^\w/.test(a[3]))a[3]=i(a[3],null,null,b);else return a=i.filter(a[3],b,c,1^f),c||d.push.apply(d,a),!1;else if(j.match.POS.test(a[0])||j.match.CHILD.test(a[0]))return!0;return a},POS:function(a){a.unshift(!0);
return a}},filters:{enabled:function(a){return!1===a.disabled&&""hidden""!==a.type},disabled:function(a){return!0===a.disabled},checked:function(a){return!0===a.checked},selected:function(a){a.parentNode&&a.parentNode.selectedIndex;return!0===a.selected},parent:function(a){return!!a.firstChild},empty:function(a){return!a.firstChild},has:function(a,b,c){return!!i(c[3],a).length},header:function(a){return/h\d/i.test(a.nodeName)},text:function(a){var b=a.getAttribute(""type""),c=a.type;return""input""===a.nodeName.toLowerCase()&&
""text""===c&&(b===c||null===b)},radio:function(a){return""input""===a.nodeName.toLowerCase()&&""radio""===a.type},checkbox:function(a){return""input""===a.nodeName.toLowerCase()&&""checkbox""===a.type},file:function(a){return""input""===a.nodeName.toLowerCase()&&""file""===a.type},password:function(a){return""input""===a.nodeName.toLowerCase()&&""password""===a.type},submit:function(a){var b=a.nodeName.toLowerCase();return(""input""===b||""button""===b)&&""submit""===a.type},image:function(a){return""input""===a.nodeName.toLowerCase()&&
""image""===a.type},reset:function(a){var b=a.nodeName.toLowerCase();return(""input""===b||""button""===b)&&""reset""===a.type},button:function(a){var b=a.nodeName.toLowerCase();return""input""===b&&""button""===a.type||""button""===b},input:function(a){return/input|select|textarea|button/i.test(a.nodeName)},focus:function(a){return a===a.ownerDocument.activeElement}},setFilters:{first:function(a,b){return 0===b},last:function(a,b,c,d){return b===d.length-1},even:function(a,b){return 0===b%2},odd:function(a,b){return 1===
b%2},lt:function(a,b,c){return b<c[3]-0},gt:function(a,b,c){return b>c[3]-0},nth:function(a,b,c){return c[3]-0===b},eq:function(a,b,c){return c[3]-0===b}},filter:{PSEUDO:function(a,b,c,d){var f=b[1],e=j.filters[f];if(e)return e(a,c,b,d);if(""contains""===f)return 0<=(a.textContent||a.innerText||B([a])||"""").indexOf(b[3]);if(""not""===f){b=b[3];c=0;for(d=b.length;c<d;c++)if(b[c]===a)return!1;return!0}i.error(f)},CHILD:function(a,b){var c,d,f,e,h,g;c=b[1];g=a;switch(c){case ""only"":case ""first"":for(;g=g.previousSibling;)if(1===
g.nodeType)return!1;if(""first""===c)return!0;g=a;case ""last"":for(;g=g.nextSibling;)if(1===g.nodeType)return!1;return!0;case ""nth"":c=b[2];d=b[3];if(1===c&&0===d)return!0;f=b[0];if((e=a.parentNode)&&(e[p]!==f||!a.nodeIndex)){h=0;for(g=e.firstChild;g;g=g.nextSibling)1===g.nodeType&&(g.nodeIndex=++h);e[p]=f}g=a.nodeIndex-d;return 0===c?0===g:0===g%c&&0<=g/c}},ID:function(a,b){return 1===a.nodeType&&a.getAttribute(""id"")===b},TAG:function(a,b){return""*""===b&&1===a.nodeType||!!a.nodeName&&a.nodeName.toLowerCase()===
b},CLASS:function(a,b){return-1<("" ""+(a.className||a.getAttribute(""class""))+"" "").indexOf(b)},ATTR:function(a,b){var c=b[1],c=i.attr?i.attr(a,c):j.attrHandle[c]?j.attrHandle[c](a):null!=a[c]?a[c]:a.getAttribute(c),d=c+"""",f=b[2],e=b[4];return null==c?""!=""===f:!f&&i.attr?null!=c:""=""===f?d===e:""*=""===f?0<=d.indexOf(e):""~=""===f?0<=("" ""+d+"" "").indexOf(e):!e?d&&!1!==c:""!=""===f?d!==e:""^=""===f?0===d.indexOf(e):""$=""===f?d.substr(d.length-e.length)===e:""|=""===f?d===e||d.substr(0,e.length+1)===e+""-"":!1},POS:function(a,
b,c,d){var f=j.setFilters[b[2]];if(f)return f(a,c,b,d)}}},G=j.match.POS,I=function(a,b){return""\\""+(b-0+1)},u;for(u in j.match)j.match[u]=RegExp(j.match[u].source+/(?![^\[]*\])(?![^\(]*\))/.source),j.leftMatch[u]=RegExp(/(^(?:.|\r|\n)*?)/.source+j.match[u].source.replace(/\\(\d+)/g,I));j.match.globalPOS=G;var m=function(a,b){a=Array.prototype.slice.call(a,0);return b?(b.push.apply(b,a),b):a};try{Array.prototype.slice.call(document.documentElement.childNodes,0)[0].nodeType}catch(J){m=function(a,b){var c=
0,d=b||[];if(""[object Array]""===E.call(a))Array.prototype.push.apply(d,a);else if(""number""===typeof a.length)for(var f=a.length;c<f;c++)d.push(a[c]);else for(;a[c];c++)d.push(a[c]);return d}}var y,v;document.documentElement.compareDocumentPosition?y=function(a,b){return a===b?(w=!0,0):!a.compareDocumentPosition||!b.compareDocumentPosition?a.compareDocumentPosition?-1:1:a.compareDocumentPosition(b)&4?-1:1}:(y=function(a,b){if(a===b)return w=!0,0;if(a.sourceIndex&&b.sourceIndex)return a.sourceIndex-
b.sourceIndex;var c,d,f=[],e=[];c=a.parentNode;d=b.parentNode;var h=c;if(c===d)return v(a,b);if(c){if(!d)return 1}else return-1;for(;h;)f.unshift(h),h=h.parentNode;for(h=d;h;)e.unshift(h),h=h.parentNode;c=f.length;d=e.length;for(h=0;h<c&&h<d;h++)if(f[h]!==e[h])return v(f[h],e[h]);return h===c?v(a,e[h],-1):v(f[h],b,1)},v=function(a,b,c){if(a===b)return c;for(a=a.nextSibling;a;){if(a===b)return-1;a=a.nextSibling}return 1});(function(){var a=document.createElement(""div""),b=""script""+(new Date).getTime(),
c=document.documentElement;a.innerHTML=""<a name='""+b+""'/>"";c.insertBefore(a,c.firstChild);if(document.getElementById(b)){j.find.ID=function(a,b,c){if(typeof b.getElementById!==""undefined""&&!c)return(b=b.getElementById(a[1]))?b.id===a[1]||typeof b.getAttributeNode!==""undefined""&&b.getAttributeNode(""id"").nodeValue===a[1]?[b]:void 0:[]};j.filter.ID=function(a,b){var c=typeof a.getAttributeNode!==""undefined""&&a.getAttributeNode(""id"");return a.nodeType===1&&c&&c.nodeValue===b}}c.removeChild(a);c=a=null})();
(function(){var a=document.createElement(""div"");a.appendChild(document.createComment(""""));if(a.getElementsByTagName(""*"").length>0)j.find.TAG=function(a,c){var d=c.getElementsByTagName(a[1]);if(a[1]===""*""){for(var f=[],e=0;d[e];e++)d[e].nodeType===1&&f.push(d[e]);d=f}return d};a.innerHTML=""<a href='#'></a>"";if(a.firstChild&&typeof a.firstChild.getAttribute!==""undefined""&&a.firstChild.getAttribute(""href"")!==""#"")j.attrHandle.href=function(a){return a.getAttribute(""href"",2)};a=null})();document.querySelectorAll&&
function(){var a=i,b=document.createElement(""div"");b.innerHTML=""<p class='TEST'></p>"";if(!(b.querySelectorAll&&b.querySelectorAll("".TEST"").length===0)){i=function(b,c,e,h){c=c||document;if(!h&&!i.isXML(c)){var g=/^(\w+$)|^\.([\w\-]+$)|^#([\w\-]+$)/.exec(b);if(g&&(c.nodeType===1||c.nodeType===9)){if(g[1])return m(c.getElementsByTagName(b),e);if(g[2]&&j.find.CLASS&&c.getElementsByClassName)return m(c.getElementsByClassName(g[2]),e)}if(c.nodeType===9){if(b===""body""&&c.body)return m([c.body],e);if(g&&
g[3]){var l=c.getElementById(g[3]);if(l&&l.parentNode){if(l.id===g[3])return m([l],e)}else return m([],e)}try{return m(c.querySelectorAll(b),e)}catch(p){}}else if(c.nodeType===1&&c.nodeName.toLowerCase()!==""object""){var g=c,n=(l=c.getAttribute(""id""))||""__sizzle__"",o=c.parentNode,k=/^\s*[+~]/.test(b);l?n=n.replace(/'/g,""\\$&""):c.setAttribute(""id"",n);if(k&&o)c=c.parentNode;try{if(!k||o)return m(c.querySelectorAll(""[id='""+n+""'] ""+b),e)}catch(q){}finally{l||g.removeAttribute(""id"")}}}return a(b,c,e,h)};
for(var c in a)i[c]=a[c];b=null}}();(function(){var a=document.documentElement,b=a.matchesSelector||a.mozMatchesSelector||a.webkitMatchesSelector||a.msMatchesSelector;if(b){var c=!b.call(document.createElement(""div""),""div""),d=false;try{b.call(document.documentElement,""[test!='']:sizzle"")}catch(f){d=true}i.matchesSelector=function(a,f){f=f.replace(/\=\s*([^'""\]]*)\s*\]/g,""='$1']"");if(!i.isXML(a))try{if(d||!j.match.PSEUDO.test(f)&&!/!=/.test(f)){var g=b.call(a,f);if(g||!c||a.document&&a.document.nodeType!==
11)return g}}catch(l){}return i(f,null,null,[a]).length>0}}})();(function(){var a=document.createElement(""div"");a.innerHTML=""<div class='test e'></div><div class='test'></div>"";if(a.getElementsByClassName&&a.getElementsByClassName(""e"").length!==0){a.lastChild.className=""e"";if(a.getElementsByClassName(""e"").length!==1){j.order.splice(1,0,""CLASS"");j.find.CLASS=function(a,c,d){if(typeof c.getElementsByClassName!==""undefined""&&!d)return c.getElementsByClassName(a[1])};a=null}}})();i.contains=document.documentElement.contains?
function(a,b){return a!==b&&(a.contains?a.contains(b):true)}:document.documentElement.compareDocumentPosition?function(a,b){return!!(a.compareDocumentPosition(b)&16)}:function(){return false};i.isXML=function(a){return(a=(a?a.ownerDocument||a:0).documentElement)?a.nodeName!==""HTML"":false};var H=function(a,b,c){for(var d,f=[],e="""",b=b.nodeType?[b]:b;d=j.match.PSEUDO.exec(a);){e=e+d[0];a=a.replace(j.match.PSEUDO,"""")}a=j.relative[a]?a+""*"":a;d=0;for(var h=b.length;d<h;d++)i(a,b[d],f,c);return i.filter(e,
f)};window.Sizzle=i})()";

        private static string sizzleScriptInjection
        {
            get
            {
                return
                    "var scriptSource = " + compiledSizzleSource + ";" +
                    "var newScript = document.createElement('script');" +
                    "newScript.type = 'text/javascript';" +
                    "if (typeof newScript.appendChild == 'function') {" +
                        "var scriptContent = document.createTextNode(scriptSource);" +
                        "newScript.appendChild(scriptContent);" +
                    "} else {" +
                        "newScript.text = scriptSource;" +
                    "}" +
                    "var headTag = document.getElementByTagName('head')[0];" +
                    "headTag.appendChild(newScript);";
            }
        }

        public static void Inject(IJavaScriptExecutor executor)
        {
            try
            {
                if ((bool)executor.ExecuteScript("return typeof Sizzle == 'undefined'"))
                {
                    executor.ExecuteScript(sizzleScriptInjection);

                    // IE requires a second execution
                    if (executor is OpenQA.Selenium.IE.InternetExplorerDriver)
                    {
                        executor.ExecuteScript(sizzleScriptInjection);
                    }
                }
            }
            catch { }
        }

        public static BySizzle Find(string selector)
        {
            return new BySizzle(selector);
        }
        
        public class BySizzle : By
        {
            private readonly string serializedSelector = null;

            public BySizzle(string selector)
            {
                var serializedSelector = JsonConvert.SerializeObject(selector);
                this.serializedSelector = serializedSelector;
            }

            public override IWebElement FindElement(ISearchContext context)
            {
                try
                {
                    return FindElements(context).First();
                }
                catch (InvalidOperationException)
                {
                    throw new NoSuchElementException("No such element");
                }
            }

            public override System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
            {
                IJavaScriptExecutor executor = context as IJavaScriptExecutor;
                Sizzle.Inject(executor);

                IEnumerable<object> matchedItems = (IEnumerable<object>)executor.ExecuteScript("return Sizzle(" + serializedSelector + ")");
                List<IWebElement> webElements = matchedItems.Cast<IWebElement>().ToList();

                return new System.Collections.ObjectModel.ReadOnlyCollection<IWebElement>(webElements);
            }
        }
    }
}